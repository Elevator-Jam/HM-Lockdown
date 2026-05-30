using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using VContainer;

[RequireComponent(typeof(Button))]
public class AbilityCooldown : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] private string abilityID;
    [SerializeField] private float cooldownTime = 5f;

    [Header("UI References")]
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color readyColor = Color.white;

    private Button abilityButton;
    private bool isOnCooldown = false;
    private AbilityManager _abilityManager;

    [Inject]
    public void Construct(AbilityManager abilityManager)
    {
        _abilityManager = abilityManager;
    }

    private void Awake()
    {
        // Ask the root scope to inject into this object
        // Assuming your LifetimeScope is in the scene, it's accessible globally
        var scope = Object.FindAnyObjectByType<GameLifetimeScope>();
        if (scope != null) {
            scope.Container.Inject(this);
        }
        
        abilityButton = GetComponent<Button>();
        
        // If no ID is provided, fallback to the GameObject's name like the old script did
        if (string.IsNullOrEmpty(abilityID))
        {
            abilityID = gameObject.name;
        }

        // Auto-register the listener so it can't be bypassed
        abilityButton.onClick.RemoveListener(OnClick); // Avoid duplicates
        abilityButton.onClick.AddListener(OnClick);
        
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 1f; // Ready state (Full)
            cooldownOverlay.color = readyColor;
        }
    }

    public void OnClick()
    {
        if (isOnCooldown) return;

        // Perform the ability
        if (cooldownOverlay != null) {
            cooldownOverlay.fillAmount = 0;
        }
        _abilityManager.PerformAbility(abilityID);
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        abilityButton.interactable = false;

        float timer = 0f;
        while (timer < cooldownTime)
        {
            timer += Time.deltaTime;
            float progress = timer / cooldownTime;

            if (cooldownOverlay != null)
            {
                // Update fill amount (filling up from bottom to top or radial)
                cooldownOverlay.fillAmount = progress;
                
                // Shift color from red to ready (white/green)
                cooldownOverlay.color = Color.Lerp(startColor, readyColor, progress);
            }

            yield return null;
        }

        // Reset state
        isOnCooldown = false;
        abilityButton.interactable = true;
        
        if (cooldownOverlay != null)
        {
            cooldownOverlay.fillAmount = 1f; // Ensure it's snapped to full
            cooldownOverlay.color = readyColor;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SelectTurret : MonoBehaviour
{
    private static SelectTurret currentSelection;

    [SerializeField] GameObject turretPrefab;
    [SerializeField] int turretValue;
    
    [Header("Visual Feedback")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color selectedColor = Color.green;
    
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null) buttonImage.color = normalColor;
    }

    public void OnClick()
    {
        // If we click the same button again, deselect it
        if (currentSelection == this)
        {
            Deselect();
            BuildingManager.Instance.SetTurretSelected(null);
            BuildingManager.Instance.SetTurretValue(0);
            return;
        }

        // Deselect the previous button
        if (currentSelection != null)
        {
            currentSelection.Deselect();
        }

        // Select this button
        currentSelection = this;
        if (buttonImage != null) buttonImage.color = selectedColor;

        BuildingManager.Instance.SetTurretSelected(turretPrefab);
        BuildingManager.Instance.SetTurretValue(turretValue);
    }

    public void Deselect()
    {
        if (buttonImage != null) buttonImage.color = normalColor;
        if (currentSelection == this) currentSelection = null;
    }
}

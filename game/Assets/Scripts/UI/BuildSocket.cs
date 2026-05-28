using UnityEngine;
using VContainer;

public class BuildSocket : MonoBehaviour
{
    [SerializeField] bool isOccupied;
    [SerializeField] GameObject OccupiedBy;
    [SerializeField] string acceptedTag;

    private CurrencyManager _currencyManager;

    [Inject]
    public void Construct(CurrencyManager currencyManager)
    {
        _currencyManager = currencyManager;
    }

    bool CanAccept(GameObject placeable, int cost)
    {
        if (isOccupied) 
        {
            Debug.Log("[BuildSocket] Build failed: Socket is already occupied.");
            return false;
        }

        if (placeable == null)
        {
            Debug.Log("[BuildSocket] Build failed: No turret selected.");
            return false;
        }

        if (placeable.tag != acceptedTag)
        {
            Debug.Log($"[BuildSocket] Build failed: Tag mismatch. Prefab tag: {placeable.tag}, Expected: {acceptedTag}");
            return false;
        }
        
        // Check if player has enough scrap
        var manager = _currencyManager ?? CurrencyManager.Instance;
        int currentScrap = manager != null ? manager.GetScrap() : 0;
        if (currentScrap < cost)
        {
            Debug.Log($"[BuildSocket] Build failed: Not enough scrap. Have: {currentScrap}, Need: {cost}");
            return false;
        }

        return true;
    }

    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color highlightColor = Color.green;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = normalColor;
    }

    public void SetHighlight(bool isHighlighted)
    {
        if (spriteRenderer != null && !isOccupied)
        {
            spriteRenderer.color = isHighlighted ? highlightColor : normalColor;
        }
    }

    public void Release()
    {
        isOccupied = false;
        this.gameObject.SetActive(true);
    }

    public void Occupy()
    {
        GameObject selectedTurret = BuildingManager.Instance.GetTurretSelected();
        int cost = BuildingManager.Instance.GetTurretValue();

        if(CanAccept(selectedTurret, cost))
        {
            var manager = _currencyManager ?? CurrencyManager.Instance;
            if (manager != null)
            {
                manager.SubtractScrap(cost);
            }
            GameObject turretInstance = Instantiate(selectedTurret, transform.position, Quaternion.identity);
            
            // Link the turret to this socket so it can release it when destroyed
            BaseTurret turretScript = turretInstance.GetComponent<BaseTurret>();
            if (turretScript != null)
            {
                turretScript.SetParentSocket(this);
            }

            isOccupied = true;
            this.gameObject.SetActive(false);
        }
    }

    public void OnClick() => Occupy();
}

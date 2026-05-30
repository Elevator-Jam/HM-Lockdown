using UnityEngine;
using VContainer;

public class BuildSocket : MonoBehaviour
{
    [SerializeField] bool isOccupied;
    [SerializeField] GameObject OccupiedBy;
    [SerializeField] string acceptedTag;

    private CurrencyManager _currencyManager;
    private BuildingManager _buildingManager;

    private void Awake() {
        // Ask the root scope to inject into this object
        // Assuming your LifetimeScope is in the scene, it's accessible globally
        var scope = Object.FindAnyObjectByType<GameLifetimeScope>();
        if (scope != null) {
            scope.Container.Inject(this);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            spriteRenderer.color = normalColor;
        }
    }

    [Inject]
    public void Construct(CurrencyManager currencyManager, BuildingManager buildingManager) {
        _currencyManager = currencyManager;
        _buildingManager = buildingManager;
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
        int currentScrap = _currencyManager.GetScrap();
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
        GameObject selectedTurret = _buildingManager.GetTurretSelected();
        int cost = _buildingManager.GetTurretValue();

        if(CanAccept(selectedTurret, cost))
        {
            _currencyManager.SubtractScrap(cost);
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

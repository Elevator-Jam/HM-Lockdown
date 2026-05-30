using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BuildSocket : MonoBehaviour
{
    [SerializeField] bool isOccupied;
    [SerializeField] GameObject OccupiedBy;
    [SerializeField] string acceptedTag;

    private CurrencyManager _currencyManager;
    private BuildingManager _buildingManager;
    private IObjectResolver _container;

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
    public void Construct(CurrencyManager currencyManager, BuildingManager buildingManager, IObjectResolver container)
    {
        _currencyManager = currencyManager;
        _buildingManager = buildingManager;
        _container = container;
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
        var manager = _currencyManager;
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
        var buildManager = _buildingManager;
        GameObject selectedTurret = buildManager != null ? buildManager.GetTurretSelected() : null;
        int cost = buildManager != null ? buildManager.GetTurretValue() : 0;

        if(CanAccept(selectedTurret, cost))
        {
            var currencyManager = _currencyManager;
            if (currencyManager != null)
            {
                currencyManager.SubtractScrap(cost);
            }
            
            GameObject turretInstance;
            if (_container != null)
            {
                turretInstance = _container.Instantiate(selectedTurret, transform.position, Quaternion.identity);
            }
            else
            {
                turretInstance = Instantiate(selectedTurret, transform.position, Quaternion.identity);
            }
            
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

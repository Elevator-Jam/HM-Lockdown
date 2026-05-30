using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

public class Scrap : MonoBehaviour
{
    [SerializeField] private int scrapValue = 10;
    [SerializeField] private float lifetime = 15f;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatAmount = 0.2f;

    private Vector3 startPos;
    private Collider2D myCollider;
    private CurrencyManager _currencyManager;

    [Inject]
    public void Construct(CurrencyManager currencyManager)
    {
        _currencyManager = currencyManager;
    }

    void Start()
    {
        startPos = transform.position;
        myCollider = GetComponent<Collider2D>();
        
        // Auto-destroy if not collected in time
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // 1. Simple floating animation
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 2. Self-contained Input Detection (Compatible with New Input System)
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

            // Check if the click happened inside our collider
            if (myCollider == Physics2D.OverlapPoint(worldPos))
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        _currencyManager.AddScrap(scrapValue);
        Destroy(gameObject);
    }
}

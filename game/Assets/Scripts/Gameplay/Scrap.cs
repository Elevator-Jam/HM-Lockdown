using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a collectible scrap pickup in the world.
/// Attach to the Scrap prefab alongside a Collider2D.
/// </summary>
public class Scrap : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector Fields
    // -------------------------------------------------------------------------

    [Header("Scrap Settings")]

    [SerializeField]
    private int _scrapValue = 10;

    [SerializeField]
    private float _lifetime = 15f;

    [Header("Float Animation")]

    [SerializeField]
    private float _floatSpeed = 1f;

    [SerializeField]
    private float _floatAmount = 0.2f;

    // -------------------------------------------------------------------------
    // Private State
    // -------------------------------------------------------------------------

    private Vector3 _startPos;
    private Collider2D _myCollider;

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    private void Start()
    {
        _startPos = transform.position;

        _myCollider = GetComponent<Collider2D>();

        if (_myCollider == null)
        {
            Debug.LogWarning("[Scrap] No Collider2D found — click detection will not work.");
        }

        // Auto-destroy if not collected within the lifetime window
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        animateFloat();
        detectTap();
    }

    // -------------------------------------------------------------------------
    // Private Helpers
    // -------------------------------------------------------------------------

    /// Function: animateFloat
    /// <summary>
    /// Purpose: Applies a sine-wave vertical offset to produce a looping float effect.
    /// </summary>
    /// <returns> Nothing </returns>
    void animateFloat()
    {
        float newY = _startPos.y + Mathf.Sin(Time.time * _floatSpeed) * _floatAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// Function: detectTap
    /// <summary>
    /// Purpose: Checks whether the player pressed on this scrap object this frame
    ///          using the New Input System, and triggers collection if so.
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: Compatible with both mouse and touch via Pointer.current.
    /// Skips the check entirely if no collider is present.
    /// </remarks>
    void detectTap()
    {
        if (_myCollider == null)
        {
            return;
        }

        if (Pointer.current == null || !Pointer.current.press.wasPressedThisFrame)
        {
            return;
        }

        Vector2 screenPos = Pointer.current.position.ReadValue();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        // Collect only if the tap landed inside this object's collider
        if (_myCollider == Physics2D.OverlapPoint(worldPos))
        {
            Collect();
        }
    }

    /// Function: Collect
    /// <summary>
    /// Purpose: Awards the scrap value to the CurrencyManager and destroys this pickup.
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: Safe to call externally (e.g. from AutoCollect).
    /// Null-checks the singleton before calling to avoid errors in edge cases
    /// where the manager has been destroyed before the scrap.
    /// </remarks>
    public void Collect()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddScrap(_scrapValue);
        }
        else
        {
            Debug.LogWarning("[Scrap] CurrencyManager instance is null — scrap value lost.");
        }

        Destroy(gameObject);
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SelectTurret : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static SelectTurret currentSelection;

    [SerializeField] GameObject turretPrefab;
    [SerializeField] int turretValue;
    
    [Header("Visual Feedback")]
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color selectedColor = Color.green;
    
    private Image buttonImage;
    private GameObject ghostInstance;
    private BuildSocket currentHoveredSocket;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null) buttonImage.color = normalColor;
    }

    public void OnClick()
    {
        SelectThisTurret();
    }

    private void SelectThisTurret()
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

    // --- Drag and Drop Logic ---

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Auto-select when starting a drag
        SelectThisTurret();

        if (turretPrefab != null)
        {
            ghostInstance = Instantiate(turretPrefab);
            SetupGhost(ghostInstance);
            UpdateGhostPosition();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (ghostInstance != null)
        {
            UpdateGhostPosition();
            DetectSocketUnderGhost();
            UpdateGhostVisuals();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ghostInstance != null)
        {
            if (currentHoveredSocket != null)
            {
                currentHoveredSocket.Occupy();
                currentHoveredSocket.SetHighlight(false);
            }

            Destroy(ghostInstance);
            ghostInstance = null;
            currentHoveredSocket = null;
        }
    }

    private void UpdateGhostPosition()
    {
        if (Pointer.current == null) return;

        Vector2 pointerPos = Pointer.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pointerPos.x, pointerPos.y, 10f));
        worldPos.z = 0;
        ghostInstance.transform.position = worldPos;
    }

    private void DetectSocketUnderGhost()
    {
        Vector2 worldPoint = ghostInstance.transform.position;
        
        // Use OverlapPointAll to find EVERYTHING at this position
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPoint);
        BuildSocket socket = null;

        foreach (var hit in hits)
        {
            socket = hit.GetComponent<BuildSocket>();
            if (socket != null) break; // Found one!
        }

        if (socket != currentHoveredSocket)
        {
            if (currentHoveredSocket != null) currentHoveredSocket.SetHighlight(false);
            currentHoveredSocket = socket;
            if (currentHoveredSocket != null) currentHoveredSocket.SetHighlight(true);
        }
    }

    private void UpdateGhostVisuals()
    {
        if (ghostInstance == null) return;

        Color targetColor = (currentHoveredSocket != null) ? Color.green : Color.red;
        targetColor.a = 0.5f;

        SpriteRenderer[] renderers = ghostInstance.GetComponentsInChildren<SpriteRenderer>();
        foreach (var rend in renderers)
        {
            rend.color = targetColor;
        }
    }

    private void SetupGhost(GameObject ghost)
    {
        // Disable all scripts on the ghost so it doesn't fight or shoot
        MonoBehaviour[] scripts = ghost.GetComponentsInChildren<MonoBehaviour>();
        foreach (var script in scripts) 
        {
            script.enabled = false;
        }

        // Disable colliders so it doesn't interfere with physics
        Collider2D[] colliders = ghost.GetComponentsInChildren<Collider2D>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }

        // Initialize transparency
        UpdateGhostVisuals();
    }
}

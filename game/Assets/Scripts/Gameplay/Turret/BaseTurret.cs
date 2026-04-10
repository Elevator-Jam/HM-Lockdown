using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BaseTurret : MonoBehaviour
{
    public enum Side 
    { 
        Both, 
        Left, 
        Right 
    }

    [SerializeField] 
    bool isPlayerControlled = false;
    [SerializeField] 
    private Side turretSide = Side.Both;
    [SerializeField] 
    List<Transform> visibleTargets = new List<Transform>();
    [SerializeField] 
    GameObject target;
    
    private Transform houseTransform;
    IRotate rotateScript;

    [Header("Lifespan Settings")]
    [SerializeField] 
    private float turretLifetime = 15f;
    private BuildSocket parentSocket;

    void Start()
    {
        // Auto-destruct after set lifetime only for automatic turrets
        if (!isPlayerControlled)
        {
            Destroy(gameObject, turretLifetime);
        }

        rotateScript = this.gameObject.GetComponent<IRotate>();
        
        // Find the House to use as the center line (Pivot: X=0)
        GameObject houseObj = GameObject.FindGameObjectWithTag("House");
        if (houseObj != null) 
        {
            houseTransform = houseObj.transform;
        }

        float centerX = houseTransform != null ? houseTransform.position.x : 0f;

        // Auto-detect side based on position relative to House
        if (turretSide == Side.Both)
        {
            turretSide = transform.position.x < centerX ? Side.Left : Side.Right;
        }

        // Mirror the secondary turrets visually if it's on the right side
        if (turretSide == Side.Right && !isPlayerControlled)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerControlled)
        {
            if (Pointer.current != null)
            {
                Vector2 screenPos = Pointer.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));
                rotateScript.Rotate(worldPos);
            }
        }
        else 
        {
            if (target != null && !IsOnCorrectSide(target.transform))
            {
                target = null;
            }

            if (target != null)
            {
                rotateScript.Rotate(target.transform.position);
            }
            else if (visibleTargets.Count > 0)
            {
                FindNewTarget();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("bullet")) return;

        // Find the Entity (works for child colliders too)
        Transform entityT = null;
        if (other.gameObject.CompareTag("Entity")) entityT = other.transform;
        else if (other.transform.parent != null && other.transform.parent.CompareTag("Entity")) entityT = other.transform.parent;

        if (entityT == null) return;

        if (!visibleTargets.Contains(entityT))
        {
            visibleTargets.Add(entityT);
        }
        
        if (IsOnCorrectSide(entityT))
        {
            SetTarget(entityT.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Transform entityT = null;
        if (other.gameObject.CompareTag("Entity")) entityT = other.transform;
        else if (other.transform.parent != null && other.transform.parent.CompareTag("Entity")) entityT = other.transform.parent;

        if (entityT == null) return;

        if(target != null && GameObject.ReferenceEquals(target, entityT.gameObject))
        {
            target = null;
        }

        visibleTargets.Remove(entityT);
    }

    private void FindNewTarget()
    {
        visibleTargets.RemoveAll(item => item == null);

        foreach (Transform t in visibleTargets)
        {
            if (t != null && IsOnCorrectSide(t))
            {
                SetTarget(t.gameObject);
                break;
            }
        }
    }

    private bool IsOnCorrectSide(Transform t)
    {
        if (turretSide == Side.Both) return true;

        float centerX = houseTransform != null ? houseTransform.position.x : 0f;

        if (turretSide == Side.Left) 
        {
            return t.position.x < centerX;
        }
        if (turretSide == Side.Right) 
        {
            return t.position.x > centerX;
        }
        return false;
    }

    public GameObject GetCurrentTarget()
    {
        return target;
    }

    private void SetTarget(GameObject newTarget)
    {
        if (target == null)
        {
            target = newTarget;
        }
    }

    public void SetParentSocket(BuildSocket socket)
    {
        parentSocket = socket;
    }

    private void OnDestroy()
    {
        if (parentSocket != null)
        {
            parentSocket.Release();
        }
    }
}

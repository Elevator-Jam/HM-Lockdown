using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class BaseTurret : MonoBehaviour
{
    [SerializeField] bool isPlayerControlled = false;
    [SerializeField] List<Transform> visibleTargets = new List<Transform>();
    [SerializeField] GameObject target;
    IRotate rotateScript;
    IFire fireScript;
    ITurret turretScript;
    void Start()
    {
        rotateScript = this.gameObject.GetComponent<IRotate>();
    }


    private void FixedUpdate()
    {
        if (isPlayerControlled)
        {
            if (Pointer.current != null)
            {
                Vector2 screenPos = Pointer.current.position.ReadValue();
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f)); // Assuming 10f depth for 2D
                rotateScript.Rotate(worldPos);
            }
        }
        else if (target != null)
        {
            rotateScript.Rotate(target.transform.position);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Entity"))
        {
            return;
        }

        visibleTargets.Add(other.gameObject.transform);
        SetTarget(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Entity"))
        {
            return;
        }
        if(target != null)
        {
            if (GameObject.ReferenceEquals(target, other.gameObject))
            {
                target = null;
            }
        }

        visibleTargets.Remove(other.gameObject.transform);
    }

    private void SetTarget(GameObject newTarget)
    {
        if (target == null)
        {
            target = newTarget;
        }
    }
}

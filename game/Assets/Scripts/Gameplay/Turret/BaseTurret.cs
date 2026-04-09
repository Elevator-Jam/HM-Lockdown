using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseTurret : MonoBehaviour
{
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
        if(target != null)
        {
            rotateScript.Rotate(target.transform);
            //fireScript.SetFirepointIdx();
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

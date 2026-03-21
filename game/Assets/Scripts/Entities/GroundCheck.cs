using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        this.gameObject.GetComponentInParent<BaseHoodlin>().enabled = true;
    }
}

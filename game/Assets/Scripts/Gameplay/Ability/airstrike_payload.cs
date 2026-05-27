using Unity.VisualScripting;
using UnityEngine;

public class airstrike_payload : MonoBehaviour
{
    // Reference to animator for explosion animation
    [SerializeField] private Animator _animator;

    // reference to child element handling enemy detection
    public EnemyDetection detect;

    // Collision detection with the ground "aka go boom".
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Fire trigger condition for explosion animation
            _animator.SetTrigger("payloadDropped");

            // Call upon collision check for all enemies in range
            // also deals damage to enemies in range here.
            detect.Detonate();
        }

        // Destroy game object after allowing time for animation to play.
        Destroy(this.gameObject, 2f);
    }

    // Helpful debugging range visual indicator.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}

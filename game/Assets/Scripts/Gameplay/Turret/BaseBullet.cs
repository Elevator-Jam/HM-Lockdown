using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Entity")
        {
            return;
        }
        other.gameObject.GetComponent<IEntity>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}

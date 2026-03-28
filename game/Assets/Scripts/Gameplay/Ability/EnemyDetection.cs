using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyDetection : MonoBehaviour
{
    // List used to store enemies in range.
    public List<Collider2D> enemies = new List<Collider2D>();


    public void Detonate()
    {
        // hit each enemy in range
        foreach (var enemy in enemies)
        {
            // check to make sure enemy still exists before calling on it
            if (enemy)
            {
                // Deal damage to each enemy in range
                IEntity entity = enemy.GetComponent<IEntity>();
                entity.TakeDamage(200);
            }
            else
            {
                Debug.Log("Enemy not found");
            }

        }
    }

    // Add enemies to the collision list as they enter range.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Entity")
        {
            enemies.Add(other);
        }

    }
}

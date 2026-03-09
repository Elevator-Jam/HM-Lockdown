using UnityEngine;

public class TestTurret : MonoBehaviour, ITurret
{


    public void SetState()
    {
        
    }


    public void EnemyDetection()
    {
        
    }


    public GameObject SetTarget(GameObject target)
    {
        return target;
    }

    public void ChangeTarget()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Entity")
        {
            return;
        }
    }
}

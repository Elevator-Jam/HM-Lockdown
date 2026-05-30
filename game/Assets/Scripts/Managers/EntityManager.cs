using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

public class EntityManager : MonoBehaviour
{
    private IObjectResolver _container;

    [Inject]
    public void Construct(IObjectResolver container)
    {
        _container = container;
    }

    // TODO: Create an optimized Object pool
    /// Function: WaveLoader
    /// <summary>
    /// Purpose: Adds all the waves into a queue
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: this makes it easier to set waves as we only need to dequeue</remarks>
    // void WaveLoader()
    // {
    //     foreach(List<GameObject> wave in WaveList)
    //     {
    //         WaveQueue.Enqueue(wave);
    //     }
    // }

    /// Function: SetState
    /// <summary>
    /// Purpose: Prepares the the next wave for spawning
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use a switch statement</remarks>
    // void SetCurrentWave()
    // {
    //     List<GameObject> currentWave = WaveQueue.Dequeue();

    //     foreach (GameObject entity in currentWave)
    //     {
    //         WaveEntities.Enqueue(entity);
    //     }
    // }

    // TODO: create a spawn timer function

    // TODO: create a spawn locator to indicate where entities can spawn based on type

    [SerializeField] List<WaveInfo> WaveSpawn = new List<WaveInfo>();
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    [SerializeField] int currentWave;
    [SerializeField] float spawnCooldownInSeconds;

    [SerializeField] bool leftSpawn = false;
    [SerializeField] Transform target;
    [System.Serializable]
    struct WaveInfo
    {
        public List<GameObject> EntityList;
    }

    Transform SetSpawnpoint()
    {
        int pointSelected = Random.Range(0, SpawnPoints.Count);

        return SpawnPoints[pointSelected];
    }

    public IEnumerator SpawnCooldown()
    {
        while (true)
        {
            int entitySelected = Random.Range(0, WaveSpawn[currentWave].EntityList.Count);
            Transform pointeSelected = SetSpawnpoint();
            
            GameObject entity;
            if (_container != null)
            {
                entity = _container.Instantiate(WaveSpawn[currentWave].EntityList[entitySelected], pointeSelected.position, Quaternion.identity);
            }
            else
            {
                entity = Instantiate(WaveSpawn[currentWave].EntityList[entitySelected], pointeSelected.position, Quaternion.identity);
            }

            entity.GetComponent<IEntity>().SetTarget(target);

            if (entity.transform.position.x < 0)
            {
                SpriteRenderer enemySprite = entity.GetComponentInChildren<SpriteRenderer>();
                enemySprite.flipX = true;
            }

            yield return new WaitForSeconds(spawnCooldownInSeconds);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EntityManager : SingletonConstructor<EntityManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }
    [SerializeField] List<List<GameObject>> WaveList = new List<List<GameObject>>();
    [SerializeField] Queue<List<GameObject>> WaveQueue = new Queue<List<GameObject>>();
    [SerializeField] Queue<GameObject> WaveEntities = new Queue<GameObject>();
    
    // TODO: Create an optimized Object pool
    /// Function: WaveLoader
    /// <summary>
    /// Purpose: Adds all the waves into a queue
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: this makes it easier to set waves as we only need to dequeue</remarks>
    void WaveLoader()
    {
        foreach(List<GameObject> wave in WaveList)
        {
            WaveQueue.Enqueue(wave);
        }
    }

    /// Function: SetState
    /// <summary>
    /// Purpose: Prepares the the next wave for spawning
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use a switch statement</remarks>
    void SetCurrentWave()
    {
        List<GameObject> currentWave = WaveQueue.Dequeue();

        foreach (GameObject entity in currentWave)
        {
            WaveEntities.Enqueue(entity);
        }
    }

    // TODO: create a spawn timer function

    // TODO: create a spawn locator to indicate where entities can spawn based on type
}

using UnityEngine;
using System.Collections;
using VContainer;
using VContainer.Unity;

public class AirStrike : MonoBehaviour, IAbility
{
    private IObjectResolver _container;

    [Inject]
    public void Construct(IObjectResolver container)
    {
        _container = container;
    }
    
    GameObject[] spellLocationsLeft;
    GameObject[] spellLocationsRight;
    void Start()
    {

        spellLocationsLeft = GameObject.FindGameObjectsWithTag("SpellLocationLeft");
        spellLocationsRight = GameObject.FindGameObjectsWithTag("SpellLocationRight");
    }
    public GameObject airstrikePayload;

    public void CastAbility(int level)
    {
        // Determine level of Airstrike ability
        switch (level)
        {
            // Start level 1 airstrike
            case 1:
                SpawnPayload(1);
                break;
            // Start level 2 airstrike
            case 2:
                SpawnPayload(2);
                break;

            default:
                break;
        }
    }

    public void SpawnPayload(int level)
    {

        int payloadWaves;

        // If ability is upgraded to level 2 drop more payloads
        if (level == 2)
        {
            payloadWaves = 10;
            StartCoroutine(dropPayload(1f));
        }
        // Otherwise just drop 3
        else
        {
            payloadWaves = 3;
            StartCoroutine(dropPayload(1f));
        }

        // Add some time between spawned payloads
        IEnumerator dropPayload(float time)
        {
            // Wait between each payload
            for (int i = 0; i < payloadWaves; i++)
            {
                spawn(i);
                yield return new WaitForSeconds(time);
                // Slightly decrease time between each wave of payloads
                time = time * .9f;

            }

        }
        void spawn(int index)
        {
            // Spawn a payload at each spell location
            // Spawn in one payload on each side
            GameObject payloadL, payloadR;
            if (_container != null)
            {
                payloadL = _container.Instantiate(airstrikePayload);
                payloadR = _container.Instantiate(airstrikePayload);
            }
            else
            {
                payloadL = Instantiate(airstrikePayload);
                payloadR = Instantiate(airstrikePayload);
            }

            // Set the position of the payloads above spell locations
            // First 3 wave should always start next to house and roll outwards
            if (index < 3)
            {
                payloadL.transform.position = spellLocationsLeft[2 - index].transform.position + new Vector3(0, 10f);
                payloadR.transform.position = spellLocationsRight[2 - index].transform.position + new Vector3(0, 10f);
            }
            // If there are more than 3 payloads (level 2/upgraded ability)
            // All waves after the first three should occur at randomly selected
            // spell locations
            else if (index >= 3)
            {
                int payloadLoc = UnityEngine.Random.Range(0, 3);
                payloadL.transform.position = spellLocationsLeft[payloadLoc].transform.position + new Vector3(0, 10f);
                payloadR.transform.position = spellLocationsRight[payloadLoc].transform.position + new Vector3(0, 10f);
            }

            Destroy(payloadL, 2f);
            Destroy(payloadR, 2f);

        }
    }

}

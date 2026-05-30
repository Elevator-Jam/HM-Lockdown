using System;
using UnityEngine;
using VContainer;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] string selectedAbility;

    AirStrike airStrikeInstance;

    [Inject]
    public void Construct()
    {
    }

    void Start()
    {
        airStrikeInstance = this.GetComponent<AirStrike>();
    }

    public void SetSelectedAbility(string ability)
    {
        selectedAbility = ability;
        PerformAbility(selectedAbility);
    }

    public void PerformAbility(string ability)
    {
        switch (ability)
        {
            case "ability_airstrike_1":
                Debug.Log("Casting airstrike level 1");
                airStrikeInstance.CastAbility(1);

                break;
            case "ability_airstrike_2":
                Debug.Log("Casting airstrike level 2");
                airStrikeInstance.CastAbility(2);
                break;
            default:
                break;

        }
    }


}

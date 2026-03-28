using System;
using UnityEngine;
using System.Collections;


public class AbilityManager : SingletonConstructor<AbilityManager>
{
    [SerializeField] string selectedAbility;

    AirStrike airStrikeInstance;


    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
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

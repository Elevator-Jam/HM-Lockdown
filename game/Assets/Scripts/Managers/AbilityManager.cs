using UnityEngine;

public class AbilityManager : SingletonConstructor<AbilityManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }
}

using UnityEngine;

public class HealthManager : SingletonConstructor<HealthManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }
    [SerializeField] int MAX_HEALTH;
    [SerializeField] int currentHealth;

    void Start()
    {
        currentHealth = MAX_HEALTH;
    }

    /// Function: AddHealth
    /// <summary>
    /// Purpose: Adds health onto the current health of the house
    /// </summary>
    /// <returns> Nothing </returns>
    /// <param name="amount">how much health to add to the current health</param>
    /// <remarks>Note: if the value is greater than the max health, set it to the max health</remarks>
    void Heal(int amount)
    {
        
    }
    /// Function: SetSprite
    /// <summary>
    /// Purpose: sets sprite based on the current health of the house
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs sprite renderers and a container at which stage it's currently at</remarks>
    void SetSprite()
    {
        
    }
}

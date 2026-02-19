using UnityEngine;

public class BuildingManager : SingletonConstructor<BuildingManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }

    /// Function: Upgrade
    /// <summary>
    /// Purpose: allows for
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Temporarily using templates until proper specifications for upgrades are made </remarks>
    void Upgrade<T>(T upgradeScript)
    {
        
    }

    /// Function: Repair
    /// <summary>
    /// Purpose: Gives back health to the building
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Temporarily using templates until proper specifications for repairs are made </remarks>
    void Repair<T>(T HPScript)
    {
        
    }

    /// Function: Recycle
    /// <summary>
    /// Purpose: Returns scrap to the currency manager
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Temporarily using templates until proper specifications for recycling are made,
    /// in addition, should reference the currency manager </remarks>
    void Recycle<T>(T valueScript)
    {
        
    }
}

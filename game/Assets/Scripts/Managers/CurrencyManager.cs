using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CurrencyManager : SingletonConstructor<CurrencyManager>
{
    [SerializeField] int _totalScraps;
    [SerializeField] List<ICurrency> currencyList = new List<ICurrency>();

    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }
    private void Start()
    {
        ResetResources();
    }

    /// Function: ResetResources
    /// <summary>
    /// Purpose: to set all values back to zero
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: if values need to be saved, look up PlayerPrefs </remarks>
    void ResetResources()
    {
        _totalScraps = 0;
    }

    /// Function: Collect
    /// <summary>
    /// Purpose: Adds the currency into their respective values
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: look into raycasts to track where the player presses,
    /// figure out how to separate based on currency type,
    /// add that value into the respective amount,
    /// and remove the item pressed on screen</remarks>
    void Collect()
    {
        
    }

    /// Function: AutoCollect
    /// <summary>
    /// Purpose: At the end of the survival state, collect all remaining values
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: use the currencyList and just call collect</remarks>
    void AutoCollect()
    {
        
    }

    /// Function: AddCurrency
    /// <summary>
    /// Purpose: Adds the currency dropped by the entity onto the list for auto collection
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Entity uses this to add the scrapped dropped on defeat</remarks>
    public void AddCurrency()
    {
        
    }

    // TODO: Create a generic collection system

    /// Function: AddScrap
    /// <summary>
    /// Purpose: Adds the scrap gained to the total
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: used once player collects scrap</remarks>
    void AddScrap()
    {
        
    }

    /// Function: SubtractScrap
    /// <summary>
    /// Purpose: Subtracts the scrap from the total
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: used once player spends scrap on buildings, upgrades, repairs, and anything that needs scraps</remarks>
    void SubtractScrap()
    {
        
    }
}

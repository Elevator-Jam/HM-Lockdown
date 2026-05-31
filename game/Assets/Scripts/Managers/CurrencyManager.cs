using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : SingletonConstructor<CurrencyManager>
{
    [SerializeField]
    private int totalScraps;

    [SerializeField]
    private List<ICurrency> currencyList = new List<ICurrency>();

    [SerializeField]
    private TMP_Text scrapText;

    [SerializeField]
    private bool reset;

    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }

    private void Start()
    {
        ResetResources();
        UpdateCurrencyText();
    }

    /// Function: ResetResources
    /// <summary>
    /// Purpose: to set all values back to zero
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: if values need to be saved, look up PlayerPrefs </remarks>
    private void ResetResources()
    {
        if (!reset)
        {
            return;
        }

        totalScraps = 0;
    }

    /// Function: Collect
    /// <summary>
    /// Purpose: Adds the currency into their respective values
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: look into raycasts to track where the player presses,
    /// figure out how to separate based on currency type,
    /// add that value into the respective amount,
    /// and remove the item pressed on screen
    /// </remarks>
    private void Collect()
    {
        // TODO: Implement currency collection logic.
    }

    /// Function: AutoCollect
    /// <summary>
    /// Purpose: At the end of the survival state, collect all remaining values
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: use the currencyList and call Collect on remaining currencies.
    /// </remarks>
    private void AutoCollect()
    {
        // TODO: Implement auto collection logic.
    }

    /// Function: AddCurrency
    /// <summary>
    /// Purpose: Adds the currency dropped by the entity onto the list for auto collection
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: Entity uses this to add the scrap dropped on defeat.
    /// </remarks>
    public void AddCurrency()
    {
        // TODO: Implement currency registration logic.
    }

    // TODO: Create a generic collection system

    /// Function: AddScrap
    /// <summary>
    /// Purpose: Adds the scrap gained to the total
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: used once player collects scrap</remarks>
    public void AddScrap(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning(
                "[CurrencyManager] Tried to add an invalid scrap value."
            );
            return;
        }

        totalScraps += value;
        UpdateCurrencyText();
    }

    /// Function: SubtractScrap
    /// <summary>
    /// Purpose: Subtracts the scrap from the total
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>
    /// Note: used once player spends scrap on buildings,
    /// upgrades, repairs, and anything that needs scraps
    /// </remarks>
    public void SubtractScrap(int value)
    {
        if (value <= 0)
        {
            Debug.LogWarning(
                "[CurrencyManager] Tried to subtract an invalid scrap value."
            );
            return;
        }

        totalScraps = Mathf.Max(0, totalScraps - value);
        UpdateCurrencyText();
    }

    /// Function: GetScrap
    /// <summary>
    /// Purpose: Returns the current amount of scrap
    /// </summary>
    /// <returns> Current scrap amount </returns>
    public int GetScrap()
    {
        return totalScraps;
    }

    /// Function: UpdateCurrencyText
    /// <summary>
    /// Purpose: Updates the scrap UI text
    /// </summary>
    /// <returns> Nothing </returns>
    private void UpdateCurrencyText()
    {
        if (scrapText == null)
        {
            Debug.LogWarning(
                "[CurrencyManager] Scrap Text is not assigned."
            );
            return;
        }

        scrapText.text = totalScraps.ToString();
    }
}
interface ICurrency
{
    public enum Type
    {
        Scrap
    }
    /// Function: SetCurrencyType
    /// <summary>
    /// Purpose: Sets the type of currency based on rarity
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs logic for how to calculate rarity</remarks>
    void SetCurrencyType();
    /// Function: SetValue
    /// <summary>
    /// Purpose: Updates the value of the currency based on type
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: use a switch statement to set</remarks>
    void SetValue();
}
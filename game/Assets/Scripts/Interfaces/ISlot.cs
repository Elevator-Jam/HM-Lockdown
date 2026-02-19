interface ISlot
{
    /// Function: IsOccupied
    /// <summary>
    /// Purpose: Checks if a slot is free of objects
    /// </summary>
    /// <returns> bool </returns>
    /// <remarks>Note: Use for turrets</remarks>
    bool IsOccupied();
    
    /// Function: AttachToSlot
    /// <summary>
    /// Purpose: Attaches object to the slot
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use transforms to set it into the location</remarks>
    void AttachToSlot();

    // TODO: create an object pool for all the turrets
    /// Function: DettachFromSlot
    /// <summary>
    /// Purpose: Removes object from the slot
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Don't forget to set current attached object to null</remarks>
    void DettachFromSlot();
}
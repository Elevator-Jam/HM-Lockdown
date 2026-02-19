interface IEntity
{
    public enum EntityState
    {
        Idle,
        Moving,
        Attacking,
        Defeated
    }

    /// Function: SetState
    /// <summary>
    /// Purpose: Updates the state of the entity
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: use Entity State to set current function logic</remarks>
    void SetState();

    /// Function: DropScrap
    /// <summary>
    /// Purpose: Upon defeat, drop scrap
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a check based on entity HP, use Currency to get the values</remarks>
    void DropScrap();

    /// Function: Move
    /// <summary>
    /// Purpose: Updates the state of the entity
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Needs a check to stop movement once entity reaches a certain range,
    /// use the x-axis to tell distance between the entity and the destination</remarks>
    void Move();

    /// Function: Attack
    /// <summary>
    /// Purpose: Entity does damage to structures made by the player
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a range check</remarks>
    void Attack();
}
interface IFire
{
    /// Function: Fire
    /// <summary>
    /// Purpose: shoot a bullet out from a barrel
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a target to begin firing, if the target dies, then change targets, if there are no targets, start idle</remarks>
    void Fire(UnityEngine.Vector3? targetPosition = null);

    // TODO: Create an Object pool for bullets fired
}
using UnityEngine;
interface ITurret
{
    public enum TurretState
    {
        Idle,
        Firing,
        Cooldown
    }

    /// Function: SetState
    /// <summary>
    /// Purpose: Updates the state of the turret
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: use Turret State to set current function logic</remarks>
    void SetState();

    /// Function: Rotate
    /// <summary>
    /// Purpose: rotate the axis of the barrel
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a target to rotate towards </remarks>
    void Rotate();

    /// Function: Fire
    /// <summary>
    /// Purpose: shoot a bullet out from a barrel
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a target to begin firing, if the target dies, then change targets, if there are no targets, start idle</remarks>
    void Fire();

    // TODO: Create an Object pool for bullets fired

    /// Function: EnemyDetection
    /// <summary>
    /// Purpose: adds an enemy into a list based on the range of the turret
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note:</remarks>
    void EnemyDetection();

    /// Function: SetTarget
    /// <summary>
    /// Purpose: sets current target
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: </remarks>
    void SetTarget();

    /// Function: ChangeTarget
    /// <summary>
    /// Purpose: updates current target when the target dies
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: if there is targets in range, set it to the nearest target</remarks>
    void ChangeTarget();
}
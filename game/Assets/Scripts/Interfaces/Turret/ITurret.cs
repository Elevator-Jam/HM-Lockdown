using UnityEngine;
public interface ITurret
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
    /// <param name="target">used to set target detected by the turret</param>
    /// <returns> GameObject </returns>
    /// <remarks>Note: </remarks>
    GameObject SetTarget(GameObject target);

    /// Function: ChangeTarget
    /// <summary>
    /// Purpose: updates current target when the target dies
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: if there is targets in range, set it to the nearest target</remarks>
    void ChangeTarget();
}
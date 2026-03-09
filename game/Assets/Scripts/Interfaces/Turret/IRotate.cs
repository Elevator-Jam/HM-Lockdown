using UnityEngine;
interface IRotate
{
    /// Function: Rotate
    /// <summary>
    /// Purpose: rotate the axis of the barrel
    /// </summary>
    /// <param name="target">the game object to rotate towards</param>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs a target to rotate towards </remarks>
    void Rotate(Transform target);
}
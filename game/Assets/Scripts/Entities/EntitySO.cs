using UnityEngine;

[CreateAssetMenu(fileName = "EntitySO", menuName = "Scriptable Objects/EntitySO")]
public class EntitySO : ScriptableObject
{

    // base attributes
    public int baseMovementSpeed;  // TODO: figure out the best minimum movement speed
    public int baseHealth;         // TODO: figure out the best minimum health
    // combat attributes
    public int baseDamage;         // TODO: figure out the best minimum damage
    public int baseAttackRange;    // TODO: figure out the best minimum range
    public int baseAttackSpeed;    // TODO: figure out the best minimum attack speed
}

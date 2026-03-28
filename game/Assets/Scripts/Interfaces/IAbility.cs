using UnityEngine;
public interface IAbility
{
    public enum Ability
    {
        AirStrike
    }

    void CastAbility(int level);

}
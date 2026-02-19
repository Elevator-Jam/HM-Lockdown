using UnityEngine;

public class GameManager : SingletonConstructor<GameManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }

    enum GameState
    {
        preparation,
        survival,
        lose,
        win
    }
    /// Function: SetState
    /// <summary>
    /// Purpose: Sets the state based on the current situation of the game
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use a switch statement</remarks>
    void SetState()
    {
        
    }
}

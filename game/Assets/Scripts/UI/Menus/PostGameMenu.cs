using UnityEngine;

public class PostGameMenu : UIPanel
{
    // Loads post game scene when the game ends
    public void OnGameFinish(int enemiesDefeated)
    {
        // Load post game scene when game is won/lost
        SetEnemiesDefeatedText(enemiesDefeated);
    }

    public void SetEnemiesDefeatedText(int amount)
    {
        // Set text object to "Enemies Defeated: " + amount
    }

    // Restarts game on button clicked
    public void OnClickPlayAgain()
    {
        GameManager.Instance.RestartGame();
        base.Hide();
    }
    
    // Returns to main menu
    public void OnClickMainMenu()
    {
        // switch ui to main menu ui
        UIManager.Instance.SwitchToMainMenu();
    }
}

using UnityEngine;

public class PostGame : MonoBehaviour
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
        // Restart main scene when "play again" button is clicked
    }
    
    // Returns to main menu
    public void OnClickMainMenu()
    {
        // Load main menu scene when "return to main menu" button is clicked
    }
}

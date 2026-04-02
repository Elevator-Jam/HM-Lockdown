using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Pauses the game and opens the pause menu when the "pause" button is clicked
    public void OnClickPause()
    {
        // Pause game 
        // open pause menu
        this.gameObject.SetActive(true);

    }

    // Resumes the game and closes the pause menu
    public void OnClickResume()
    {
        // Close pause menu
        this.gameObject.SetActive(false);
        //resume game
    }

    // Return to main menu when "exit" button is clicked
    public void OnClickExit()
    {
        // Check if game is paused
        // Load main menu scene
    }

    // Opens settings menu when "settings" button is clicked
    public void OnClickSettings()
    {
        // Check if game is paused
        // Open settings menu
    }
}

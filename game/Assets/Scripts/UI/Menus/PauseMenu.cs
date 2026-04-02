using UnityEngine;

public class PauseMenu : UIPanel
{
    // Pauses the game and opens the pause menu when the "pause" button is clicked
    public void OnClickPause()
    {
        // show pause menu
        base.Show();
        // pause game
    }

    // Resumes the game and closes the pause menu
    public void OnClickResume()
    {
        // Close pause menu
        UIManager.Instance.CloseTopUI();
        //resume game
    }

    // Return to main menu when "exit" button is clicked
    public void OnClickExit()
    {
        // finish/restart game
        // switch main menu UI
        UIManager.Instance.SwitchToMainMenu();
    }

    // Opens settings menu when "settings" button is clicked
    public void OnClickSettings()
    {
        // pause game
        // Open settings menu UI
        UIManager.Instance.SwitchToSettingsMenu();
    }
}

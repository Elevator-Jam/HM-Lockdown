using UnityEngine;

public class PauseMenu : UIPanel
{
    // Pauses the game and opens the pause menu when the "pause" button is clicked
    public void OnClickRestart()
    {
        // show pause menu
        base.Hide();
        // restart game
        GameManager.Instance.RestartGame();
    }

    // Resumes the game and closes the pause menu
    public void OnClickResume()
    {
        // Close pause menu
        UIManager.Instance.CloseTopUI();
        //resume game
        GameManager.Instance.UnpauseGame();
    }

    // Return to main menu when "exit" button is clicked
    public void OnClickExit()
    {
        // finish game
        GameManager.Instance.EndGame();
        // switch main menu UI
        UIManager.Instance.SwitchToMainMenu();
    }

    // Opens settings menu when "settings" button is clicked
    public void OnClickSettings()
    {
        // pause game
        GameManager.Instance.PauseGame();
        // Open settings menu UI
        UIManager.Instance.SwitchToSettingsMenu();
    }
}

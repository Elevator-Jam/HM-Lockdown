using UnityEngine;
using VContainer;

public class PauseMenu : UIPanel
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    [Inject]
    public void Construct(GameManager gameManager, UIManager uiManager)
    {
        _gameManager = gameManager;
        _uiManager = uiManager;
    }

    // Pauses the game and opens the pause menu when the "pause" button is clicked
    public void OnClickRestart()
    {
        // show pause menu
        base.Hide();
        // restart game
        _gameManager.RestartGame();
    }

    // Resumes the game and closes the pause menu
    public void OnClickResume()
    {
        // Close pause menu
        _uiManager.CloseTopUI();
        //resume game
        _gameManager.UnpauseGame();
    }

    // Return to main menu when "exit" button is clicked
    public void OnClickExit()
    {
        // finish game
        _gameManager.EndGame();
        
        // switch main menu UI
        _uiManager.SwitchToMainMenu();
    }

    // Opens settings menu when "settings" button is clicked
    public void OnClickSettings()
    {
        // pause game
        _gameManager.PauseGame();
        
        // Open settings menu UI
        _uiManager.SwitchToSettingsMenu();
    }
}

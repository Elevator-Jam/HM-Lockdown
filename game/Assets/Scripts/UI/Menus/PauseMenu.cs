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
        var manager = _gameManager;
        if (manager != null) {
            manager.RestartGame();
        }
    }

    // Resumes the game and closes the pause menu
    public void OnClickResume()
    {
        // Close pause menu
        var uiManager = _uiManager;
        if (uiManager != null) uiManager.CloseTopUI();
        //resume game
        var gameManager = _gameManager;
        if (gameManager != null) gameManager.UnpauseGame();
    }

    // Return to main menu when "exit" button is clicked
    public void OnClickExit()
    {
        // finish game
        var gameManager = _gameManager;
        if (gameManager != null) {
            gameManager.EndGame();
        }
        // switch main menu UI
        var uiManager = _uiManager;
        if (uiManager != null) {
            uiManager.SwitchToMainMenu();
        }
    }

    // Opens settings menu when "settings" button is clicked
    public void OnClickSettings()
    {
        // pause game
        var gameManager = _gameManager;
        if (gameManager != null) {
            gameManager.PauseGame();
        }
        // Open settings menu UI
        var uiManager = _uiManager;
        if (uiManager != null) {
            uiManager.SwitchToSettingsMenu();
        }
    }
}

using UnityEngine;
using VContainer;

public class MainMenu : UIPanel
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    [Inject]
    public void Construct(GameManager gameManager, UIManager uiManager) {
        _gameManager = gameManager;
        _uiManager = uiManager;
    }
    
    public void OnClickPlay()
    {
        // hide ui
        base.Hide();
        // start game
        _gameManager.StartGame();
    }

    // Opens settings menu when the "settings" button is clicked
    public void OnClickSettings()
    {
        // Open settings menu
        _uiManager.SwitchToSettingsMenu();
    }

    // Quits game when the "exit" button is clicked
    public void OnClickExit()
    {
        _gameManager.ExitGame();
    }

    // Opens a menu that displays credits when the "credits" button is clicked
    public void OnClickCredits()
    {
        _uiManager.SwitchToCredits();
    }
}

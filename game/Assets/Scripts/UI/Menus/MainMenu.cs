using UnityEngine;
using VContainer;

public class MainMenu : UIPanel
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    [Inject]
    public void Construct(GameManager gameManager, UIManager uiManager)
    {
        _gameManager = gameManager;
        _uiManager = uiManager;
    }
    
    public void OnClickPlay()
    {
        // hide ui
        base.Hide();
        // start game
        var manager = _gameManager;
        if (manager != null) {
            manager.StartGame();
        }
        else {
            Debug.LogWarning("no gamee manager");
        }
    }

    // Opens settings menu when the "settings" button is clicked
    public void OnClickSettings()
    {
        // Open settings menu
        var manager = _uiManager;
        if (manager != null) {
            manager.SwitchToSettingsMenu();
        }
    }

    // Quits game when the "exit" button is clicked
    public void OnClickExit()
    {
        var manager = _gameManager;
        if (manager != null) {
            manager.ExitGame();
        }
    }

    // Opens a menu that displays credits when the "credits" button is clicked
    public void OnClickCredits()
    {
        var manager = _uiManager;
        if (manager != null) {
            manager.SwitchToCredits();
        }
    }
}

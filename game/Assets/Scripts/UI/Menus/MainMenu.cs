using UnityEngine;

public class MainMenu : UIPanel
{
    
    public void OnClickPlay()
    {
        // hide ui
        base.Hide();
        // start game
        GameManager.Instance.StartGame();
    }

    // Opens settings menu when the "settings" button is clicked
    public void OnClickSettings()
    {
        // Open settings menu
        UIManager.Instance.SwitchToSettingsMenu();
    }

    // Quits game when the "exit" button is clicked
    public void OnClickExit()
    {
        GameManager.Instance.ExitGame();
    }

    // Opens a menu that displays credits when the "credits" button is clicked
    public void OnClickCredits()
    {
        UIManager.Instance.SwitchToCredits();
    }
}

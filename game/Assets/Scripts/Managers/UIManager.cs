using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UIManager : SingletonConstructor<UIManager>
{
    [SerializeField]
    private List<UIPanel> allPanels;

    private Dictionary<UIPanel.PanelID, UIPanel> panels;
    private Stack<UIPanel> panelStack;

    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
        panels = new Dictionary<UIPanel.PanelID, UIPanel>();
        foreach (var panel in allPanels)
        {
            panels[panel.panelID] = panel;
        }
        panelStack = new Stack<UIPanel>();
    }

    public void SwitchToMainMenu()
    {
        SwitchUI(UIPanel.PanelID.MainMenu);
    }
    public void SwitchToPauseMenu()
    {
        SwitchUI(UIPanel.PanelID.PauseMenu);
    }
    public void SwitchToPostGameMenu()
    {
        SwitchUI(UIPanel.PanelID.PostGameMenu);
    }
    public void SwitchToSettingsMenu()
    {
        SwitchUI(UIPanel.PanelID.SettingsMenu);
    }
    public void SwitchToGameInProgress()
    {
        SwitchUI(UIPanel.PanelID.GameInProgress);
    }

    public void SwitchToCredits()
    {
        SwitchUI(UIPanel.PanelID.CreditsMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /// Function: SwitchUI
    /// <summary>
    /// Purpose: closes top UI and opens panel id UI
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note:</remarks>
    public void SwitchUI(UIPanel.PanelID panelID)
    {
        //deactivate current ui panel
        CloseTopUI();
        //activate panelID
        OpenUI(panelID);

    }

    /// Function: Closes all UI opened 
    /// <summary>
    /// Purpose: Hides all UI from the player
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: </remarks>
    private void CloseAllUIs(List<UIPanel.PanelID> panelIDs)
    {
        while (panelStack.Count > 0)
        {
            panelStack.Pop().Hide();
        }
    }


    /// Function: OpenUI
    /// <summary>
    /// Purpose: Open UI on top of stack it doesn't close previously open
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note:</remarks>
    private void OpenUI(UIPanel.PanelID panelID)
    {
        if (!panels.ContainsKey(panelID))
        {
            UnityEngine.Debug.LogWarning($"Panel {panelID} not found.");
            return;
        }
        UIPanel panel = panels[panelID];
        if (panelStack.Count > 0)
        {
            panelStack.Peek().Hide(); // hide previous UI
        }
        panelStack.Push(panel); // add panel to stack
        panel.Show(); // show new UI
    }


    public void CloseTopUI()
    {
        if (panelStack.Count == 0)
        {
            return;
        }
        UIPanel topPanel = panelStack.Pop();
        topPanel.Hide();

        if (panelStack.Count > 0)
        {
            panelStack.Peek().Show();
        }
    }


}

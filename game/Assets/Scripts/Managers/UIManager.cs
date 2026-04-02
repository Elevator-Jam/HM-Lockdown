using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SingletonConstructor<UIManager>
{

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject postGameMenu;
    [SerializeField]
    private GameObject settingsMenu;

    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }

    /// Function: SwitchUI
    /// <summary>
    /// Purpose: Swaps one UI to another
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note:</remarks>
    void SwitchUI()
    {
        
    }

    /// Function: DisableUIs
    /// <summary>
    /// Purpose: Hides the UI from the player
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: </remarks>
    void DisableUIs()
    {
        
    }
    /// Function: EnableUIs
    /// <summary>
    /// Purpose: Shows UI to the player
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note:</remarks>
    void EnableUIs()
    {
        
    }

    public void SwitchUI2()
    {
        
    }
}

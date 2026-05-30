using UnityEngine;
using VContainer;

public class PostGameMenu : UIPanel
{
    [SerializeField]
    private CanvasGroup winCanvasGroup;
    [SerializeField]
    private CanvasGroup loseCanvasGroup;

    private GameManager _gameManager;
    private UIManager _uiManager;

    [Inject]
    public void Construct(GameManager gameManager, UIManager uiManager)
    {
        _gameManager = gameManager;
        _uiManager = uiManager;
    }

    protected override void Awake()
    {
        base.Awake();
        GameObject gObjWin = GameObject.FindWithTag("Win_Panel");
        winCanvasGroup = gObjWin.GetComponent<CanvasGroup>();
        GameObject gObjLose = GameObject.FindWithTag("Lose_Panel");
        loseCanvasGroup = gObjLose.GetComponent<CanvasGroup>();
    }

    public void ShowWin()
    {
        OnShow(winCanvasGroup);
    }

    public void ShowLose()
    {
        OnShow(loseCanvasGroup);
    }

    private void OnShow(CanvasGroup cGroup)
    {
        cGroup.alpha = 1f;
        cGroup.interactable = true;
        cGroup.blocksRaycasts = true;
    }

    private void OnHide(CanvasGroup cGroup)
    {
        cGroup.alpha = 0f;
        cGroup.interactable = false;
        cGroup.blocksRaycasts = false;
    }

    // Loads post game scene when the game ends
    public void OnGameFinish(int enemiesDefeated)
    {
        // Load post game scene when game is won/lost
        SetEnemiesDefeatedText(enemiesDefeated);
    }

    public void SetEnemiesDefeatedText(int amount)
    {
        // Set text object to "Enemies Defeated: " + amount
    }

    // Restarts game on button clicked
    public void OnClickRestart()
    {
        var manager = _gameManager;
        if (manager != null) {
            manager.RestartGame();
        }
        base.Hide();
    }
    
    // Returns to main menu
    public void OnClickMainMenu()
    {
        // switch ui to main menu ui
        var manager = _uiManager;
        if (manager != null) {
            manager.SwitchToMainMenu();
        }
    }

    public void OnClickExit()
    {
        var manager = _gameManager;
        if (manager != null) {
            manager.ExitGame();
        }
    }
}

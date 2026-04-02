using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{

    public enum PanelID
    {
        None,
        MainMenu,
        PauseMenu,
        PostGameMenu,
        SettingsMenu,
        GameInProgress,
        CreditsMenu
    };

    public PanelID panelID;

    protected CanvasGroup canvasGroup;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}

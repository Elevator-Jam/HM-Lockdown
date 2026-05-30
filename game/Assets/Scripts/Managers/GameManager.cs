using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using VContainer;

public class GameManager : MonoBehaviour
{
    [Header("UI Manager")]
    private UIManager _uiManager;

    [Header("Entity Manager")]
    private EntityManager _entityManager;

    [Inject]
    public void Construct(UIManager uiManager, EntityManager entityManager)
    {
        _uiManager = uiManager;
        _entityManager = entityManager;
    }


    [Header("Game Timer")]
    [SerializeField] Slider timerSlider;
    [SerializeField] float currentTimer;
    [Header("Preparation Time")]
    [SerializeField] int preparationTimeInMinutes;
    [SerializeField] int preparationTimeInSeconds;
    [Header("Survival Time")]
    [SerializeField] int survivalTimeInMinutes;
    [SerializeField] int survivalTimeInSeconds;
    [Header("Wave Tracker")]
    [SerializeField] int currentWave;

    [SerializeField] int currentEnemies;

    [Header("Background Images")]
    [SerializeField] GameObject backgroundNight;
    [SerializeField] GameObject backgroundDay;
    void SetTimer(int TimeInMinutes, int TimeInSeconds)
    {
        int totalTime = (TimeInMinutes * 60) + TimeInSeconds;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        currentTimer = totalTime;
        timerSlider.minValue = 0f;
    }

    public enum GameState
    {
        not_started, //game is in the main menu
        preparation,
        survival,
        paused,
        lose,
        win
    }

    [Header("Game State")]
    private GameState previousGameState;
    [SerializeField] public GameState gameState;
    
    /// Function: SetState
    /// <summary>
    /// Purpose: Sets the state based on the current situation of the game
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use a switch statement</remarks>
    Coroutine spawnRoutine;
    public void SetState()
    {
        var uiManager = _uiManager;
        var entityManager = _entityManager;

        switch (gameState)
        {
            case GameState.not_started: // game is in the main menu
                if (uiManager != null) uiManager.SwitchToMainMenu();
                Time.timeScale = 0;
                break;

            case GameState.preparation:
                Time.timeScale = 1;
                currentWave++;
                SetTimer(preparationTimeInMinutes, preparationTimeInSeconds);
                if (spawnRoutine != null)
                {
                    StopCoroutine(spawnRoutine);
                }
                break;

            case GameState.survival:
                Time.timeScale = 1;
                SetTimer(survivalTimeInMinutes, survivalTimeInSeconds);
                if (entityManager != null) spawnRoutine = StartCoroutine(entityManager.SpawnCooldown());
                break;
                
            case GameState.paused:
                Time.timeScale = 0;
                if (uiManager != null) uiManager.SwitchToPauseMenu();
                break;

            case GameState.lose:
                // Pause game
                Time.timeScale = 0;
                // Display post game menu lost
                if (uiManager != null) uiManager.SwitchToPostGameMenu(false);
                break;

            case GameState.win:
                // Pause game
                Time.timeScale = 0;
                // Display post game menu win
                if (uiManager != null) uiManager.SwitchToPostGameMenu(true);
                break;
            default:
                break;
        }
    }

    void ChangeState()
    {
        switch (gameState)
        {
            // Change game state to survival phase
            // Set background to "day"
            case GameState.preparation:
                gameState = GameState.survival;
                backgroundNight.SetActive(false);
                break;

            // If the survival phase is not the final wave
            // Change game state to preparation
            // Set background to "night"
            case GameState.survival:
                if (currentWave >= 3)
                {
                    gameState = GameState.win;
                }
                else
                {
                    gameState = GameState.preparation;
                    backgroundNight.SetActive(true);
                }
                break;
        }

        SetState();
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public void StartGame()
    {
        gameState = GameState.preparation;
        currentWave = 0;
        SetState();
    }

    public void PauseGame()
    {
        if (gameState == GameState.paused)
        {
            return;
        }
        previousGameState = gameState;
        gameState = GameState.paused;
        SetState();
    }

    public void UnpauseGame()
    {
        var uiManager = _uiManager;
        if (uiManager != null) uiManager.CloseTopUI();
        if (gameState != GameState.paused) 
        {
            return;
        }
        gameState = previousGameState;
        SetState();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;

        // Stop all local coroutines (like spawnRoutine) before reload
        StopAllCoroutines();

        // Reset persistent static variables
        BaseHoodlin.ResetStatics();

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndGame()
    {
        gameState = GameState.lose;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    private void Start()
    {
        SetState();
    }

    private void Update()
    {
        if (Keyboard.current != null && 
            Keyboard.current.escapeKey.wasPressedThisFrame) //pause/unpause game using escape key
        {
            if (gameState == GameState.paused) 
            {
                UnpauseGame();
            } 
            else if (gameState == GameState.survival || 
                    gameState == GameState.preparation) 
            {
                PauseGame();
            }
        }

        if (gameState == GameState.paused) 
        {
            return;
        }

        if (currentTimer < 0)
        {
            // Continue changing states if win/loss state is not active
            if (gameState != GameState.win && gameState != GameState.lose)
            {
                ChangeState();
            }
        }

        currentTimer -= Time.deltaTime;
        timerSlider.value = currentTimer;
    }
}

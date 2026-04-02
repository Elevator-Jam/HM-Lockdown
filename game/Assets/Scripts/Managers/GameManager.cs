using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GameManager : SingletonConstructor<GameManager>
{
    private void Awake()
    {
        ConstructSingleton(this); // ! DO NOT DELETE
    }
    [Header("Game Timer")]
    [SerializeField] Slider timerSlider;
    [SerializeField] int currentTimer;
    [Header("Preparation Time")]
    [SerializeField] int preparationTimeInMinutes;
    [SerializeField] int preparationTimeInSeconds;
    [Header("Survival Time")]
    [SerializeField] int survivalTimeInMinutes;
    [SerializeField] int survivalTimeInSeconds;
    [Header("Wave Tracker")]
    [SerializeField] int currentWave;
    void SetTimer(int TimeInMinutes, int TimeInSeconds)
    {
        int totalTime = (TimeInMinutes * 60) + TimeInSeconds;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        currentTimer = totalTime;
        timerSlider.minValue = 0f;
    }

    IEnumerator Timer()
    {
        while (currentTimer >= 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimer--;
            timerSlider.value = currentTimer;
        }
    }
    enum GameState
    {
        preparation,
        survival,
        lose,
        win
    }

    [Header("Game State")]
    [SerializeField] GameState gameState;
    /// Function: SetState
    /// <summary>
    /// Purpose: Sets the state based on the current situation of the game
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: Use a switch statement</remarks>
    Coroutine spawnRoutine;
    void SetState()
    {
        switch (gameState)
        {
            case GameState.preparation:
                currentWave++;
                SetTimer(preparationTimeInMinutes, preparationTimeInSeconds);
                StartCoroutine(Timer());
                if(spawnRoutine != null)
                {
                    StopCoroutine(spawnRoutine);
                }
                break;
            case GameState.survival:
                SetTimer(survivalTimeInMinutes, survivalTimeInSeconds);
                StartCoroutine(Timer());
                spawnRoutine = StartCoroutine(EntityManager.Instance.SpawnCooldown());
                break;
            case GameState.lose:
                break;
            case GameState.win:
                break;
            default:
                break;
        }
    }

    void ChangeState()
    {
        switch (gameState)
        {
            case GameState.preparation:
                gameState = GameState.survival;
                break;
            case GameState.survival:
                gameState = GameState.preparation;
                break;
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public void PauseGame()
    {
        Debug.LogWarning("function not yet implemented");
    }

    public void UnpauseGame()
    {
        Debug.LogWarning("function not yet implemented");
    }

    public void RestartGame()
    {
        Debug.LogWarning("function not yet implemented");
    }

    private void Start()
    {
        SetState();
    }

    private void Update()
    {
        if (currentTimer < 0)
        {
            ChangeState();
            SetState();
        }
    }
}


using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int MAX_HEALTH;
    [SerializeField] int currentHealth;
    [SerializeField] Slider healthSlider;
    
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        healthSlider.maxValue = MAX_HEALTH;
        healthSlider.value = MAX_HEALTH;

        currentHealth = MAX_HEALTH;
    }

    /// Function: Heal
    /// <summary>
    /// Purpose: Adds health onto the current health of the house
    /// </summary>
    /// <returns> Nothing </returns>
    /// <param name="amount">how much health to add to the current health</param>
    /// <remarks>Note: if the value is greater than the max health, set it to the max health</remarks>
    public void Heal(int amount)
    {

    }
    /// Function: TakeDamage
    /// <summary>
    /// Purpose: Reduce health onto the current health of the house
    /// </summary>
    /// <returns> Nothing </returns>
    /// <param name="amount">how much health to reduce to the current health</param>
    /// <remarks>Note: if the value is less than 0, set it to zero</remarks>
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            // call lose sequence here
            _gameManager.gameState = GameManager.GameState.lose;
            _gameManager.SetState();
        }
        healthSlider.value = currentHealth;
    }
    /// Function: SetSprite
    /// <summary>
    /// Purpose: sets sprite based on the current health of the house
    /// </summary>
    /// <returns> Nothing </returns>
    /// <remarks>Note: needs sprite renderers and a container at which stage it's currently at</remarks>
    void SetSprite()
    {

    }
}

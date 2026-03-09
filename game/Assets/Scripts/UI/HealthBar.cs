using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    [SerializeField] int currentHP;
    [SerializeField] int setHP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = setHP;
        healthSlider.maxValue = setHP;
        healthSlider.value = setHP;
    }

    
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        healthSlider.value = currentHP;
    }
}

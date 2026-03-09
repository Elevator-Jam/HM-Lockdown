using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class RoundTimer : MonoBehaviour
{
    [SerializeField] Slider timerSlider;
    [SerializeField] int setTimeInMinutes;
    [SerializeField] int setTimeInSeconds;
    [SerializeField] int currentTimer;

    private void Start()
    {
        SetTimer();
        StartCoroutine(Timer());
    }

    void SetTimer()
    {
        int totalTime = (setTimeInMinutes * 60) + setTimeInSeconds;
        currentTimer = totalTime;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        timerSlider.minValue = 0f;
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            currentTimer--;
            timerSlider.value = currentTimer;

            if(currentTimer <= 0f)
            {
                SetTimer();
            }
        }
    }
}

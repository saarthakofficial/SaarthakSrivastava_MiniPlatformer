using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text bestTimeText;

    public float timer;
    public float bestTime = 0;

    void Start()
    {
        timer = PlayerPrefs.GetFloat("StartTimer", 0);
        bestTime = PlayerPrefs.GetFloat("Best" + GameManager.instance.currentLevel, 0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        int bestMinutes = Mathf.FloorToInt(bestTime / 60);
        int bestSeconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = string.Format("Best: {0:00}:{1:00}", bestMinutes, bestSeconds);
    }

    public void SetBestTime()
    {
        if ((PlayerPrefs.GetFloat("Best" + GameManager.instance.currentLevel, 0) == 0) || timer < PlayerPrefs.GetFloat("Best" + GameManager.instance.currentLevel, 0)){
            bestTime = timer;
            PlayerPrefs.SetFloat("Best" + GameManager.instance.currentLevel, bestTime);
        }
    }
}
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text bestTimeText;

    public float timer;
    public float bestTime = 0;

    void Start(){
        timer = PlayerPrefs.GetFloat("StartTimer", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);
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
        Debug.Log(timer + " " + minutes + " " + seconds);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        bestTimeText.text = string.Format("Best: {0:00}:{1:00}", bestTime / 60, bestTime % 60);
    }

    public void SetBestTime(){
        PlayerPrefs.SetFloat("BestTime", timer);
        bestTime = timer;
    }
}
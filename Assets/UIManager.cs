using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animation transition;
    [SerializeField] TMP_Text level1BestText;
    [SerializeField] TMP_Text level2BestText;
    float bestTime1;
    float bestTime2;
    // Start is called before the first frame update
    void Start()
    {
        bestTime1 = PlayerPrefs.GetFloat("Best1", 0);
        bestTime2 = PlayerPrefs.GetFloat("Best2", 0);

        int best1Minutes = Mathf.FloorToInt(bestTime1 / 60);
        int best1Seconds = Mathf.FloorToInt(bestTime1 % 60);
        level1BestText.text = string.Format("{0:00}:{1:00}", best1Minutes, best1Seconds);
        
        int best2Minutes = Mathf.FloorToInt(bestTime2 / 60);
        int best2Seconds = Mathf.FloorToInt(bestTime2 % 60);
        level2BestText.text = string.Format("{0:00}:{1:00}", best2Minutes, best2Seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartButton(){
        transition.Play("End");
        Invoke("RestartGame", 1f);
    }

    public void RestartGame(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State{
    WaitArea,
    PlayArea,
    DashArea,
    Ending,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerObj;
    public int currentLevel;
    public Vector2 spawnPoint;
    public State currentState;
    public Animation transition;
    public TimerUI timerUI;
    void Awake(){
        instance = this;
    }
   
    void Start()
    {
        if (currentLevel == 1){
            spawnPoint = new Vector2(PlayerPrefs.GetFloat("spawnX", -30), PlayerPrefs.GetFloat("spawnY", -3));
        }
        else{
            spawnPoint = new Vector2(PlayerPrefs.GetFloat("spawnX", 3), PlayerPrefs.GetFloat("spawnY", -2.5f));
        }
        playerObj.transform.position = spawnPoint;
        transition.Play("Start");
        currentState = State.WaitArea;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(){
        Destroy(playerObj, 0.15f);
        transition.Play("End");
        Invoke("RestartGame", 1f);
    }

    public void RestartGame(){
        SceneManager.LoadScene(0);
    }
}

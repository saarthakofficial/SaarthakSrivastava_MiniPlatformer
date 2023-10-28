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
    public State currentState;
    public Animation transition;
    void Awake(){
    instance = this;
    }
   
    void Start()
    {
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

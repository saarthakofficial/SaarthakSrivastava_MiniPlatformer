using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State{
    Waiting,
    Playing,
    Ending,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public State currentState;
    void Awake(){
    instance = this;
    }
   
    void Start()
    {
        currentState = State.Waiting;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

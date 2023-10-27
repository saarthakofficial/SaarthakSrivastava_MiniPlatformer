using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    PlayerController player;
    
    void Awake(){
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(){
        anim.SetTrigger("Jump");
    }

    public void Land(){
        anim.SetTrigger("Land");
    }
}

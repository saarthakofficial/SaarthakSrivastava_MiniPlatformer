using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TextType{
        START,
        CHECKPOINT,
        END,
    }
public class TextTrigger : MonoBehaviour
{
    
    [SerializeField] TextType textType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            GetComponent<Animation>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this.gameObject, 1f);  
            if (textType == TextType.START){
                GameManager.instance.currentState = State.PlayArea;
            }
            else if (textType == TextType.CHECKPOINT){
                PlayerPrefs.SetFloat("spawnX", other.transform.position.x);
                PlayerPrefs.SetFloat("spawnY", other.transform.position.y);
                PlayerPrefs.SetFloat("StartTimer", GameManager.instance.timerUI.timer);
                GameManager.instance.currentState = State.PlayArea;

            }
            else if (textType == TextType.END){
                PlayerPrefs.DeleteKey("spawnX");
                PlayerPrefs.DeleteKey("spawnY");
                PlayerPrefs.DeleteKey("StartTimer");
                GameManager.instance.timerUI.SetBestTime();
                PlayerPrefs.SetFloat("Best"+GameManager.instance.currentLevel, GameManager.instance.timerUI.bestTime);
                GameManager.instance.transition.Play("End");
                // GameManager.instance.LoadNext();
                GameManager.instance.LevelFinish();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float range;
    float startTime;
    float initialPos;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        initialPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        float newPos = initialPos + range * Mathf.Sin(t * speed);
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            other.transform.parent = null;
        }
    }
}


using UnityEngine;

public class RotatingSquares : MonoBehaviour
{
    [Range(-2f,2f)]
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, 0f, rotateSpeed);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            other.transform.rotation = new Quaternion(0,0,0,0);
        }
    }
    
}

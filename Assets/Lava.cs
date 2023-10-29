
using UnityEngine;

public class Lava : MonoBehaviour
{
    Transform playerTransform;
    public float waveSpeed = 1.0f;
    public float waveHeight = 0.1f;
    
    Vector3[] originalVertices;
    Mesh mesh;

    [SerializeField] float moveSpeed;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        playerTransform = GameManager.instance.playerObj.transform;
    }

    void Update()
    {
        if (GameManager.instance.currentState == State.PlayArea){
            if (playerTransform.position.y - transform.position.y > 30f){
                transform.position = new Vector3(transform.position.x, playerTransform.position.y - 30f, transform.position.z);
            }
            transform.position += new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
        }
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].z = originalVertices[i].z + Mathf.Sin(Time.time * waveSpeed + originalVertices[i].x) * waveHeight;
        }
        mesh.vertices = vertices;
    }

    
}
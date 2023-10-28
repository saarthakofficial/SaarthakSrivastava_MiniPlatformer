using UnityEngine;

public class Lava : MonoBehaviour
{
    public float waveSpeed = 1.0f;
    public float waveHeight = 0.1f;
    
    private Vector3[] originalVertices;
    private Mesh mesh;

    [SerializeField] float moveSpeed;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        if (GameManager.instance.currentState == State.PlayArea){
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
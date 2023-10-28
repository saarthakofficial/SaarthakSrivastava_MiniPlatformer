using System.Collections;
using UnityEngine;

public class GrappleHook : MonoBehaviour {
    LineRenderer line;

    [SerializeField] LayerMask grapplableMask;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;

    [SerializeField] Vector3 offset;
    bool isGrappling = false;
    [HideInInspector] public bool retracting = false;

    Vector2 target;


    private void Start() {
        line = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !isGrappling && GetComponent<PlayerController>()._jumping) {
            StartGrapple();
        }

        if (retracting) {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position + offset);

            if (Vector2.Distance(transform.position, target) < 0.5f) {
                retracting = false;
                isGrappling = false;
                line.enabled = false;
            }
        }
    }

    private void StartGrapple() {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (transform.position + offset);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, direction, maxDistance, grapplableMask);

        if (hit.collider != null) {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple() {
        float t = 0;
        float time = 10;

        line.SetPosition(0, transform.position + offset);
        line.SetPosition(1, transform.position); 

        Vector2 newPos;

        while (t < time) {
            newPos = Vector2.Lerp(transform.position, target, t / time);
            line.SetPosition(0, transform.position + offset);
            line.SetPosition(1, newPos);
            yield return null;
            t += grappleShootSpeed * Time.deltaTime;
        }
        
        line.SetPosition(1, target);
        retracting = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float smoothSpeed = 5.0f;
    [SerializeField] float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, playerTransform.position.y + yOffset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null){
            if (GameManager.instance.currentState == State.DashArea){
                Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
            else{
                Vector3 targetPosition = new Vector3(0, playerTransform.position.y + yOffset, transform.position.z);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
        }
    }
}

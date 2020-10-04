using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] public Transform camTarget;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 3.0f, -10.0f);
    [SerializeField] private float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = camTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
        transform.position += (smoothedPosition - transform.position) * 0.2f;
        //transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10.0f);
    }
}

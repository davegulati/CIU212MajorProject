using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform sen;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Awake()
    {
        sen = GameObject.Find("Sen").transform;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = transform.position = sen.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(sen);
    }
}

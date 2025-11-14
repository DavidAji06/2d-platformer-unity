using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // Player to follow
    public Vector3 offset;         // Optional: camera offset from player
    public float smoothSpeed = 5f; // Smoothness of follow

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

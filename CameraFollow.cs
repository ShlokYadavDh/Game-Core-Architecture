using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Drag your Cube Prince here!
    public Vector3 offset;          // The distance between Camera and Prince
    public float smoothSpeed = 0.125f; // How fast the "spring" follows

    void LateUpdate()
    {
        // LateUpdate runs AFTER the player moves, preventing "jitter"
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
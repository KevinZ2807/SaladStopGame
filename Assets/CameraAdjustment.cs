using UnityEngine;

public class CameraAdjustment : MonoBehaviour
{
    public Transform target;        // The target the camera is following (usually the player)
    public float bufferDistance = 0.5f;  // A small buffer to stop exactly at the surface of the object
    public float minDistance = 1.0f;     // Minimum distance the camera can be from the target
    public float maxDistance = 4.0f;     // Maximum distance the camera can be from the target
    public LayerMask collisionLayer;     // Layer on which collision will be detected

    private Vector3 dir;            // The direction from the target to the camera
    private float currentDistance;  // Current distance from the camera to the target

    void Start()
    {
        dir = transform.position - target.position;
        dir.Normalize();
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        Vector3 desiredCameraPos = target.position + dir * maxDistance;
        RaycastHit hit;

        // Cast a ray from the target to the desired camera position
        if (Physics.Raycast(target.position, dir, out hit, maxDistance + bufferDistance, collisionLayer))
        {
            // Adjust the distance if there is a hit, keeping a buffer from the hit object
            currentDistance = Mathf.Clamp(hit.distance - bufferDistance, minDistance, maxDistance);
        }
        else
        {
            // If nothing obstructs the ray, set the distance to maximum
            currentDistance = maxDistance;
        }

        // Set the camera position to the adjusted distance along the direction
        transform.position = target.position + dir * currentDistance;
    }
}

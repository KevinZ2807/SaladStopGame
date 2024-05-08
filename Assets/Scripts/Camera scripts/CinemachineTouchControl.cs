using UnityEngine;
using Cinemachine;

/// <summary>
/// Controls a Cinemachine FreeLook camera with touch input for rotation on the right half of the screen
/// and pinch-to-zoom functionality. Includes scroll wheel simulation for zooming in the Unity Editor.
/// </summary>
//[RequireComponent(typeof(CinemachineFreeLook))]
public class CinemachineTouchControl : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float rotationSpeed = 0.1f;
    public float zoomSpeed = 2f;
    public float scrollZoomSpeed = 10f;  // Speed of zoom using the scroll wheel

    private float initialDistance;
    private float initialFOV;

    private void Start()
    {
        if (freeLookCamera == null)
        {
            Debug.LogError("FreeLook Camera is not assigned.", this);
            enabled = false;
            return;
        }

        initialFOV = freeLookCamera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            HandleRotation();
        }
        else if (Input.touchCount == 2)
        {
            HandlePinchToZoom();
        }
        
        HandleScrollWheelZoom();
    }

    private void HandleRotation()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.position.x > Screen.width / 2)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                // Horizontal rotation
                freeLookCamera.m_XAxis.Value += touch.deltaPosition.x * rotationSpeed;

                // Vertical rotation
                float delta = -touch.deltaPosition.y * rotationSpeed * 0.1f; // Scale down vertical sensitivity
                freeLookCamera.m_YAxis.Value = Mathf.Clamp(freeLookCamera.m_YAxis.Value + delta, 0f, 1f); // Clamping to ensure Y axis stays within valid bounds
            }
        }
    }

    private void HandlePinchToZoom()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
        {
            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
        }
        else if (touchZero.phase == TouchPhase.Moved || touchOne.phase is TouchPhase.Moved)
        {
            float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
            float difference = initialDistance - currentDistance;

            freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(initialFOV + difference * zoomSpeed, 10, 100);
        }
    }

    /// <summary>
    /// Handles zooming using the mouse scroll wheel.
    /// </summary>
    private void HandleScrollWheelZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            freeLookCamera.m_Lens.FieldOfView = Mathf.Clamp(freeLookCamera.m_Lens.FieldOfView - scroll * scrollZoomSpeed, 10, 100);
        }
    }
}

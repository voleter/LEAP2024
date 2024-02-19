using UnityEngine;
using Oculus.Interaction; // Ensure Oculus Interaction namespace is included

public class VRUIPositioner : MonoBehaviour
{
    public Transform userCamera; // Assign your VR Camera
    public float distanceInFront = 2.0f; // How far in front of the camera the UI should appear
    public float smoothFactor = 0.1f; // Adjust for smoother movement
    public OVRHand ovrHand; // Assign the OVRHand component for pinch detection
    public Transform uiElement; // Assign your UI element's transform in the inspector

    private bool isDragging = false;

    void Update()
    {
        bool isPinching = ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        // Check for pinch gesture to start or stop dragging
        if (isPinching && pinchStrength > 0.5f && !isDragging)
        {
            BeginDrag();
        }
        else if (!isPinching && isDragging)
        {
            EndDrag();
        }

        // If currently dragging, update the UI position
        if (isDragging)
        {
            UpdateUIPosition();
        }
    }

    void BeginDrag()
    {
        isDragging = true;
    }

    void EndDrag()
    {
        isDragging = false;
    }

    void UpdateUIPosition()
    {
        // Calculate the target position in front of the user
        Vector3 targetPosition = userCamera.position + userCamera.forward * distanceInFront;

        // Smoothly move the UI element to the target position
        uiElement.position = Vector3.Lerp(uiElement.position, targetPosition, smoothFactor);

        // Ensure the UI element faces the user
        uiElement.rotation = Quaternion.LookRotation(uiElement.position - userCamera.position);
    }
}

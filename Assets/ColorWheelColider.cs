using UnityEngine;
using Oculus.Interaction;

public class ColorWheelRotation : MonoBehaviour
{
    public OVRHand hand;
    private Vector3 lastHandPosition;
    private bool isHandOverWheel = false;
    private bool isPinching = false;
    private bool isHandClose = false;
    private void Update()
    {
        // Check the proximity of the hand to the color wheel
        isHandClose = IsHandCloseToWheel();

        bool isCurrentlyPinching = isHandClose && hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        if (isCurrentlyPinching)
        {
            if (!isPinching)
            {
                isPinching = true;
                lastHandPosition = hand.transform.position;
            }
            else
            {
                RotateWheel(hand.transform.position);
            }
        }
        else if (isPinching)
        {
            isPinching = false;
            lastHandPosition = Vector3.zero;
        }
    }

    private void RotateWheel(Vector3 handPosition)
    {
        if (lastHandPosition != Vector3.zero)
        {
            Vector3 direction = handPosition - lastHandPosition;
            float rotationAmount = Vector3.Dot(direction, transform.up) * 10f; // Adjust the sensitivity as needed
            transform.Rotate(0, 0, rotationAmount);
            lastHandPosition = handPosition;
        }
    }

    private bool IsHandCloseToWheel()
    {
        // Check if the hand is within a certain distance to the color wheel
        // You can adjust the 'interactionDistance' to the appropriate value for your setup
        float interactionDistance = 0.1f;
        return Vector3.Distance(hand.transform.position, transform.position) < interactionDistance;
    }


private void RotateWheelBasedOnHandMovement(Vector3 handPosition)
    {
        if (lastHandPosition == Vector3.zero) return; // No last position to compare to

        // Calculate hand movement
        Vector3 handMovement = handPosition - lastHandPosition;

        // Convert hand movement into a rotation around the Z-axis
        float rotationAmount = handMovement.x * 10f; // The factor '10f' controls the sensitivity
        transform.Rotate(0, 0, rotationAmount);
    }

    // Call this method when the hand enters the collider of the color wheel
    public void OnHandEnterWheel()
    {
        isHandOverWheel = true;
    }

    // Call this method when the hand exits the collider of the color wheel
    public void OnHandExitWheel()
    {
        isHandOverWheel = false;
    }
}

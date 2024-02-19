using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Oculus.Interaction;

public class VRColorWheelController : MonoBehaviour
{
    public OVRHand handLeft, handRight; // Assign in the Inspector
    public RectTransform colorWheel; // Assign your color wheel's RectTransform here
    public Image colorIndicator; // Shows the selected color
    public UnityEvent<Color> onColorSelected; // Event when a color is selected

    private OVRHand leftOVRHand, rightOVRHand; // For accessing pinch strength and tracking status
    private Vector3 lastHandPosition;
    private bool isDragging = false;

    void Start()
    {
        leftOVRHand = handLeft.GetComponent<OVRHand>();
        rightOVRHand = handRight.GetComponent<OVRHand>();
        if (onColorSelected == null)
            onColorSelected = new UnityEvent<Color>();
    }

    void Update()
    {
        CheckForPinchAndRotate();
    }

    private void CheckForPinchAndRotate()
    {
        // Check if either hand is pinching
        if (leftOVRHand.IsTracked && leftOVRHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.7f)
        {
            ProcessInteraction(handLeft.transform.position, leftOVRHand);
        }
        else if (rightOVRHand.IsTracked && rightOVRHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) > 0.7f)
        {
            ProcessInteraction(handRight.transform.position, rightOVRHand);
        }
        else
        {
            isDragging = false;
        }
    }

    private void ProcessInteraction(Vector3 handPosition, OVRHand ovrHand)
    {
        if (!isDragging)
        {
            lastHandPosition = handPosition;
            isDragging = true;
            return;
        }

        // Determine rotation amount based on hand movement
        float rotationAmount = CalculateRotationAmount(handPosition);
        RotateColorWheel(rotationAmount);

        // Optional: On pinch release, select color
        if (ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index) < 0.7f)
        {
            SelectColorUnderPointer(handPosition);
            isDragging = false;
        }

        lastHandPosition = handPosition;
    }

    private float CalculateRotationAmount(Vector3 currentHandPosition)
    {
        // Simplified example: Calculate rotation based on horizontal hand movement
        float deltaX = currentHandPosition.x - lastHandPosition.x;
        return deltaX * 100; // Adjust multiplier for sensitivity
    }

    private void RotateColorWheel(float rotationAmount)
    {
        colorWheel.Rotate(new Vector3(0, 0, -rotationAmount));
    }

    private void SelectColorUnderPointer(Vector3 handPosition)
    {
        // Here, you would add logic to determine the color under the pointer when the pinch is released
        // This example assumes you've converted the 3D hand position to a 2D position on the color wheel
        // and retrieved the color at that position.

        // Placeholder: Invoke color selected event with a test color
        Color selectedColor = Color.red; // Placeholder for actual color selection logic
        onColorSelected.Invoke(selectedColor);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPaletteController : MonoBehaviour
{
    public RectTransform picker;
    public Image pickedColorImage;
    public UnityEvent<Color> OnColorChange;
    public VRDrawHandTracking vrDrawHandTracking;
    public Material colorWheelMat;
    public int totalNumberOfColors = 24;
    public int wheelsCount = 2;
    public float startingAngle = 0;
    public bool controlSV = false;
    public float minimumSatValStep = 0.01f;
    public float minimumSaturation = 0.25f;
    public float maximumSaturation = 1;
    public float minimumValue = 0.25f;
    public float maximumValue = 1;
    public UnityEvent<float> OnHueChange;
    private Vector3 lastHandPos;
    private bool isPinching = false;
    public OVRHand ovrHand;
    private Camera mainCamera;
    [SerializeField]
    private float rotationSpeed = 50f;
    void Start()
    {
        mainCamera = Camera.main; // Assuming the main camera is tagged as MainCamera
        if (vrDrawHandTracking == null)
        {
            vrDrawHandTracking = FindObjectOfType<VRDrawHandTracking>();
        }
        OnColorChange.AddListener(HandleColorChange);
    }
    private void HandleColorChange(Color newColor)
    {
        // Update the drawing color in VRDrawHandTracking
        if (vrDrawHandTracking != null)
        {
            vrDrawHandTracking.UpdateDrawingColor(newColor);
        }
    }

    void Update()
    {
       
            bool currentlyPinching = ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            if (currentlyPinching && !isPinching)
            {
                isPinching = true;
                // Logic for starting an action, like rotating the color wheel
            }
            else if (!currentlyPinching && isPinching)
            {
                isPinching = false;
                // Logic for ending the action, like finalizing the color selection
            }
        

        if (ovrHand.IsTracked)
        {
            bool isPinching = ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
            float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

            if (isPinching && pinchStrength > 0.5f) // Assuming pinch to rotate
            {
                Vector3 handPos = ovrHand.transform.position;
                if (lastHandPos != Vector3.zero)
                {
                    // Calculate rotation based on hand movement
                    Vector3 direction = handPos - lastHandPos;
                    float angle = direction.x * rotationSpeed; // Determine your rotationSpeed
                    transform.Rotate(0, 0, angle);
                }
                lastHandPos = handPos;
            }
            else
            {
                lastHandPos = Vector3.zero;
            }
        }
    }


    private void UpdatePickerPosition(Vector3 handMovement)
    {
        // Convert hand movement in 3D space to a 2D movement on the screen
        Vector2 screenMovement = new Vector2(handMovement.x, handMovement.y) * 1000; // Adjust multiplier as needed for sensitivity

        // Assuming the picker's movement directly correlates to hand movement
        Vector2 newPickerPosition = picker.anchoredPosition + screenMovement;
        picker.anchoredPosition = newPickerPosition;

        // Update the color based on the new picker position
        // This is a simplified example, actual implementation may vary
        UpdateColor(newPickerPosition);
    }

    private void UpdateColor(Vector2 pickerPosition)
    {
        // Implement color updating logic here
        // This might involve converting pickerPosition to a color value
        Color newColor = Color.white; // Placeholder for actual color calculation
        pickedColorImage.color = newColor;

        OnColorChange.Invoke(newColor);
    }
}

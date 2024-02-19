using UnityEngine;
using Oculus.Interaction; // Make sure Oculus Interaction SDK is correctly set up

public class VRPinchWindowDragger : MonoBehaviour
{
    public OVRHand ovrHand;
    public RectTransform dragArea;
    public RectTransform dragObject;
    public bool topOnDrag = true;

    private Vector3 originalPanelLocalPosition;
    private bool isDragging = false;
    private float smoothingFactor = 0.1f; // Adjust this value to control the speed

    void Update()
    {
        // Detect pinch start
        if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isDragging)
        {
            StartDrag();
        }
        // Update drag position based on hand movement
        else if (isDragging && ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            UpdateDrag();
        }
        // Detect pinch end
        else if (isDragging && !ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            isDragging = false;
        }
    }

    void StartDrag()
    {
        originalPanelLocalPosition = dragObject.localPosition;
        isDragging = true;
        if (topOnDrag)
        {
            dragObject.SetAsLastSibling();
        }
    }

    void UpdateDrag()
    {
        Vector3 handPosition = Camera.main.WorldToScreenPoint(ovrHand.PointerPose.position);
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(dragArea, handPosition, null, out localPointerPosition))
        {
            Vector3 targetPosition = originalPanelLocalPosition + (Vector3)localPointerPosition;
            dragObject.localPosition = Vector3.Lerp(dragObject.localPosition, targetPosition, smoothingFactor);
        }
    }
}

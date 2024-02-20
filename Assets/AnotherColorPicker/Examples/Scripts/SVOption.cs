using UnityEngine;
using UnityEngine.UI;

public class SVOption : MonoBehaviour
{
    public Image BackgroundImage;
    public Image ForegroundImage;
    public ColorPaletteController ColorPalette;
    private Collider optionCollider;
    private OVRHand hand;

    void Start()
    {
        hand = FindObjectOfType<OVRHand>();
        optionCollider = GetComponent<Collider>();
    }

    void Update()
    {
        CheckHandGestureOverOption();
    }

    private void CheckHandGestureOverOption()
    {
        // Detect if the hand is pinching and if it's within the bounds of this option.
        bool isPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        if (isPinching && IsHandOverOption())
        {
            // Toggle option logic here. This is a simplification.
            BackgroundImage.enabled = !BackgroundImage.enabled;
            ForegroundImage.enabled = !ForegroundImage.enabled;
            // Apply changes to ColorPalette based on option.
            ColorPalette.controlSV = BackgroundImage.enabled; // Example toggle
        }
    }

    private bool IsHandOverOption()
    {
        // Implement checking if hand is over this option's collider.
        // This method needs customization based on your scene setup.
        Ray ray = new Ray(hand.transform.position, Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider == optionCollider;
        }
        return false;
    }
}

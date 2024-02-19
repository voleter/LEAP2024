using UnityEngine;
using Oculus.Interaction;

public class PinkyPinchShowUI : MonoBehaviour
{
    public OVRHand hand;
    public GameObject uiPanel; // Assign your UI panel in the inspector

    // Update is called once per frame
    void Update()
    {
        if (hand == null || uiPanel == null) return;

        // Check if the pinky finger pinch strength exceeds a certain threshold (e.g., 0.7)
        if (hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky) > 0.7f)
        {
            // Show the UI panel
            uiPanel.SetActive(true);
        }
        else
        {
            // Optional: Hide the UI panel when the pinch is released
            uiPanel.SetActive(false);
        }
    }
}

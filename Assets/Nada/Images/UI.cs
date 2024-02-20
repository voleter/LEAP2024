using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI : MonoBehaviour
{
    public OVRHand Hand;
    public UnityEvent OnPinchStart; // Event triggered when pinch starts
    public UnityEvent OnPinchEnd; // Event triggered when pinch ends
    private bool isPinching = false;

    void Update()
    {
        // Check if the hand is pinching
        bool pinch = Hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float pinchStrength = Hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        // Detect pinch start
        if (!isPinching && pinch && pinchStrength > 0.5f) // Adjust strength threshold as needed
        {
            isPinching = true;
            OnPinchStart.Invoke(); // Invoke pinch start event
        }
        // Detect pinch end
        else if (isPinching && (!pinch || pinchStrength <= 0.5f))
        {
            isPinching = false;
            OnPinchEnd.Invoke(); // Invoke pinch end event
        }
    }
}
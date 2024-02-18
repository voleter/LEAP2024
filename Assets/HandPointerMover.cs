// using UnityEngine;

// public class HandPointerMover : MonoBehaviour
// {
//     public GameObject pointerObject; // Assign your pointer GameObject here

//     void Update()
//     {
//         OVRHand hand = FindObjectOfType<OVRHand>(); // Or reference it more efficiently if possible
//         if (hand != null && hand.IsTracked)
//         {
//             Vector3 fingerTipPosition = hand.GetFingerTipPosition(OVRHand.HandFinger.Index, true);
//             pointerObject.transform.position = fingerTipPosition;
//         }
//     }
// }
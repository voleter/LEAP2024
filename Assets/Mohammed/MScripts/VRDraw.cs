using UnityEngine;
using System.Collections.Generic;
// Ensure you're using the correct namespace for Oculus functionality
using Oculus.Interaction;

public class VRDrawHandTracking : MonoBehaviour
{
    public OVRHand ovrHand;
    // Add a reference to the OVRSkeleton component
    public OVRSkeleton ovrSkeleton;
    public GameObject linePrefab;
    private LineRenderer currentLineRenderer;
    public float lineWidth = 0.01f;
    public Material lineMaterial;
    private bool isDrawing = false;
    private List<GameObject> lines = new List<GameObject>();

    void Update()
    {
        // Check if the hand is tracked
        if (ovrHand.IsTracked)
        {
            // Use pinch strength to determine if the user is pinching
            float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

            if (pinchStrength > 0.5f) // Adjust pinch strength threshold as needed
            {
                if (!isDrawing)
                {
                    Vector3 startPosition = GetFingerTipPosition();
                    StartDrawing(startPosition);
                }
                else
                {
                    UpdateDrawing();
                }
            }
            else
            {
                isDrawing = false;
            }
        }
    }

    void StartDrawing(Vector3 startPosition)
    {
        isDrawing = true;
        GameObject lineObj = Instantiate(linePrefab, startPosition, Quaternion.identity);
        currentLineRenderer = lineObj.GetComponent<LineRenderer>();

        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.material = lineMaterial;
        currentLineRenderer.SetPosition(0, startPosition);
        currentLineRenderer.SetPosition(1, startPosition);

        lines.Add(lineObj);
    }

    void UpdateDrawing()
    {
        if (currentLineRenderer != null && isDrawing)
        {
            Vector3 currentPosition = GetFingerTipPosition();
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, currentPosition);
        }
    }

    // This method now correctly fetches the finger tip position
    private Vector3 GetFingerTipPosition()
    {
        // Find the index tip bone
        OVRSkeleton.BoneId boneId = OVRSkeleton.BoneId.Hand_IndexTip;
        foreach (var bone in ovrSkeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                return bone.Transform.position;
            }
        }
        return Vector3.zero; // Fallback in case the bone isn't found
    }
}

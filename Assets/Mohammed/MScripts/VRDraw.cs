using UnityEngine;
using System.Collections.Generic;
using Oculus.Interaction;

public class VRDrawHandTracking : MonoBehaviour
{
    public OVRHand ovrHand;
    public OVRSkeleton ovrSkeleton;
    public GameObject linePrefab;
    private LineRenderer currentLineRenderer;
    public float lineWidth = 0.01f;
    public Material lineMaterial;
    private bool isDrawing = false;
    private bool isErasing = false;
    private List<GameObject> lines = new List<GameObject>();

    void Update()
    {
        if (ovrHand.IsTracked)
        {
            isErasing = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Middle) > 0.5f;
            if (isErasing)
            {
                Vector3 eraserPosition = GetFingerTipPosition();
                EraseNearestLineSegment(eraserPosition);
                return;
            }
            float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            if (pinchStrength > 0.5f)
            {
                Vector3 fingerTipPosition = GetFingerTipPosition();
                if (!isDrawing)
                {
                    StartDrawing(fingerTipPosition);
                }
                else
                {
                    UpdateDrawing(fingerTipPosition);
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

    void UpdateDrawing(Vector3 currentPosition)
    {
        if (currentLineRenderer != null && isDrawing)
        {
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, currentPosition);
        }
    }

    private Vector3 GetFingerTipPosition()
    {
        OVRSkeleton.BoneId boneId = OVRSkeleton.BoneId.Hand_IndexTip;
        foreach (var bone in ovrSkeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                return bone.Transform.position;
            }
        }
        return Vector3.zero;
    }

    void EraseNearestLineSegment(Vector3 eraserPosition)
    {
        foreach (GameObject line in lines)
        {
            LineRenderer lr = line.GetComponent<LineRenderer>();
            for (int i = 0; i < lr.positionCount; i++)
            {
                Vector3 position = lr.GetPosition(i);
                if (Vector3.Distance(eraserPosition, position) < 0.05f) // Assuming a small threshold for erasing
                {
                    lr.SetPosition(i, new Vector3(position.x, position.y, -1000)); // Move the point out of view
                }
            }
        }
    }
}

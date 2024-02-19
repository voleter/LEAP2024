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
                EraseNearestLine();
                return;
            }
            float pinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
            if (pinchStrength > 0.5f)
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

    void EraseNearestLine()
    {
        Vector3 handPosition = GetFingerTipPosition();
        GameObject nearestLine = null;
        float nearestDistance = float.MaxValue;
        foreach (GameObject line in lines)
        {
            float distance = Vector3.Distance(handPosition, line.GetComponent<LineRenderer>().GetPosition(0));
            if (distance < nearestDistance)
            {
                nearestLine = line;
                nearestDistance = distance;
            }
        }
        if (nearestLine != null)
        {
            lines.Remove(nearestLine);
            Destroy(nearestLine);
        }
    }
}

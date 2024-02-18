using UnityEngine;
using System.Collections.Generic;
using Oculus.Interaction;

public class VRDraw : MonoBehaviour
{
    public OVRHand ovrHand;
    public GameObject linePrefab;
    private LineRenderer currentLineRenderer;
    public float lineWidth = 0.01f;
    public Material lineMaterial;
    private bool isDrawing = false;
    private List<GameObject> lines = new List<GameObject>();
    private OVRSkeleton skeleton;

    void Start()
    {
        skeleton = ovrHand.GetComponent<OVRSkeleton>();
    }

    void Update()
    {
        if (ovrHand.IsTracked)
        {
            if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                if (!isDrawing)
                {
                    Vector3 startPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
                    StartDrawing(startPosition);
                }
                else
                {
                    UpdateDrawing();
                }
            }
            else if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Middle) && ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb))
            {
                // Switch to erasing mode
                EraseDrawing();
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
            Vector3 currentPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
            currentLineRenderer.positionCount++;
            currentLineRenderer.SetPosition(currentLineRenderer.positionCount - 1, currentPosition);
        }
    }

    void EraseDrawing()
    {
        Vector3 handPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_WristRoot].Transform.position;
        foreach (GameObject line in new List<GameObject>(lines))
        {
            // Check if the hand is close enough to the line to erase it
            if (Vector3.Distance(handPosition, line.transform.position) < lineWidth * 2) // Adjust this distance as needed
            {
                lines.Remove(line);
                Destroy(line);
            }
        }
    }
}

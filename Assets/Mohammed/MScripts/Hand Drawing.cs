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
            DrawWithHandTracking();
        }
    }

    void DrawWithHandTracking()
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
        else
        {
            isDrawing = false;
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
}
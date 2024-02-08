using UnityEngine;
using System.Collections.Generic;

public class SimpleVRDrawHandTracking : MonoBehaviour
{
    public OVRHand ovrHand; // Assign this via the Unity Inspector to the hand you want to track
    public GameObject linePrefab; // Assign a prefab with a LineRenderer component
    private LineRenderer currentLineRenderer;
    public float lineWidth = 0.01f; // Set the desired line width
    public Material lineMaterial; // Assign the material for the line
    private bool isDrawing = false;
    private List<GameObject> lines = new List<GameObject>(); // Stores all drawn lines
    private OVRSkeleton skeleton;

    void Start()
    {
        // Attempt to get the OVRSkeleton component from the assigned hand
        skeleton = ovrHand.GetComponent<OVRSkeleton>();
    }

    void Update()
    {
        // Check if the hand is being tracked
        if (ovrHand.IsTracked)
        {
            DrawWithHandTracking();
        }
    }

    void DrawWithHandTracking()
    {
        // Use the pinch gesture to start and continue drawing
        if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (!isDrawing)
            {
                // Capture the starting position of the pinch
                Vector3 startPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
                StartDrawing(startPosition);
            }
            else
            {
                // Continue drawing from the last position
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

        // Initialize LineRenderer settings
        currentLineRenderer.startWidth = lineWidth;
        currentLineRenderer.endWidth = lineWidth;
        currentLineRenderer.material = lineMaterial;
        currentLineRenderer.SetPosition(0, startPosition); // Set the first position to the pinch position
        currentLineRenderer.SetPosition(1, startPosition); // Initially, the end position is the same as the start

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

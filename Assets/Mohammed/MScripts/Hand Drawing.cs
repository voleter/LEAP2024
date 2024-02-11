using UnityEngine;
using System.Collections.Generic;

public class VRDraw : MonoBehaviour
{
    public OVRHand ovrHand;
    public GameObject linePrefab;
    private LineRenderer currentLineRenderer;
    public float lineWidth = 0.01f;
    public Material lineMaterial;
    private bool isDrawing = false;
    private bool isErasing = false;
    private List<GameObject> lines = new List<GameObject>();
    private OVRSkeleton skeleton;
    public LayerMask canvasLayer;

    void Start()
    {
        skeleton = ovrHand.GetComponent<OVRSkeleton>();
    }

    void Update()
    {
        if (ovrHand.IsTracked)
        {
            if (CheckForErasingGesture())
            {
                isErasing = true;
                EraseLine();
            }
            else if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index) && !isErasing)
            {
                RaycastHit hit;
                if (Physics.Raycast(skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position, Vector3.forward, out hit, Mathf.Infinity, canvasLayer))
                {
                    if (hit.collider.gameObject.CompareTag("Canvas"))
                    {
                        Vector3 startPosition = hit.point;
                        if (!isDrawing)
                        {
                            StartDrawing(startPosition);
                        }
                        else
                        {
                            UpdateDrawing(startPosition);
                        }
                    }
                }
            }
            else
            {
                isDrawing = false;
                isErasing = false;
            }
        }
    }

    void StartDrawing(Vector3 startPosition)
    {
        if (isErasing) return;

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

    bool CheckForErasingGesture()
    {
        return ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Middle) && ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Thumb);
    }

    void EraseLine()
    {
        Vector3 handPosition = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        for (int i = lines.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(handPosition, lines[i].transform.position) < 0.05f)
            {
                Destroy(lines[i]);
                lines.RemoveAt(i);
            }
        }
    }
}

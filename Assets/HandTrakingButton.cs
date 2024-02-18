using UnityEngine;

public class HandTrackingButton : MonoBehaviour
{
    public GameObject canvasToActivate; // Assign in inspector
    public float activationDistance = 0.05f; // Distance in meters for activation

    private OVRSkeleton skeleton;

    void Start()
    {
        // Attempt to get the OVRSkeleton component from the hand
        skeleton = GetComponentInChildren<OVRSkeleton>();
    }

    void Update()
    {
        if (skeleton != null && skeleton.Bones != null)
        {
            // Assume IndexTip is the fingertip of the index finger
            OVRSkeleton.BoneId fingerTipBoneId = OVRSkeleton.BoneId.Hand_IndexTip;
            int boneIndex = (int)fingerTipBoneId;

            if (boneIndex < skeleton.Bones.Count)
            {
                Vector3 fingerTipPosition = skeleton.Bones[boneIndex].Transform.position;
                float distance = Vector3.Distance(fingerTipPosition, transform.position);

                if (distance <= activationDistance)
                {
                    // Activate the canvas
                    canvasToActivate.SetActive(true);
                }
            }
        }
    }
}
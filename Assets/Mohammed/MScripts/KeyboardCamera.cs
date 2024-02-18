using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using NumericsVector3 = System.Numerics.Vector3;

public class KeyboardFollower : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 offsetFromCamera; 

    void Update()
    {
        if (cameraTransform != null)
        {
            
            transform.position = cameraTransform.position + cameraTransform.rotation * offsetFromCamera;
            
            transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
        }
    }
}

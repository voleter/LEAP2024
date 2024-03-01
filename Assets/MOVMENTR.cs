using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class movementR : MonoBehaviour
{
    public float speed;
    public float inputH;
    public float inputV;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movment = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        transform.Translate(movment * speed * Time.deltaTime);

    }
}
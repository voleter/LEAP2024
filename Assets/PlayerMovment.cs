 using System;
 using System.Collections;
 using System.Collections.Generic;
 using Unity.VisualScripting;
 using UnityEngine;
 using UnityEngine.InputSystem;

 public class PlayerMovment : MonoBehaviour
 {
     public CharacterController controller; 
     public float speed = 12f;

     public float gravity = -9.81f;

     public Transform groundcheck;

     public float groundDistance= 0.4f; 
     public LayerMask groundMask; 

     Vector3 velocity; 
     bool isgrounded ;

     public float jumphight = 3f;
     // Start is called before the first frame update

     // Update is called once per frame
     void Update()
     {
         isgrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

         if (isgrounded && velocity.y < 0)
         {
             velocity.y = -2f ;
         }
         float x = Input.GetAxis("Horizontal");
         float z = Input.GetAxis("Vertical");

         Vector3 move = transform.right* x + transform.forward * z;

         controller.Move(move * speed * Time.deltaTime);

     if (Input.GetKeyDown(KeyCode.Space))
         {
             velocity.y= Mathf.Sqrt(jumphight * -2f * gravity);
         }

         velocity.y += gravity * Time.deltaTime; 

         controller.Move(velocity * Time.deltaTime);


     }
 }


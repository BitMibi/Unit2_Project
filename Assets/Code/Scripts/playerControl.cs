using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    private Rigidbody rb;
   
    //Movement Variables
    private float movementX;
    private float movementY;
    public float speed;


    //Moving objects variables
    private bool holding = false;
    public GameObject liftable;
    private FixedJoint fixedJoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fixedJoint = GetComponent<FixedJoint>();
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();

        movementX = movement.x;
        movementY = movement.y;

    }

    void OnInteract()
    {
        if (!holding && liftable.CompareTag("PickUpTag"))
        {
            holding = true;
            pickUpObjects();
        }
        else
        {
            holding = false;
            dropObjects();
        }
    }
    //Used to pick up objects to the top of the players head
    void pickUpObjects()
    {
        fixedJoint.connectedBody = liftable.GetComponent<Rigidbody>();
        fixedJoint.anchor = transform.position + new Vector3(0, 1, 0);
        liftable.transform.position = fixedJoint.anchor;
    }

    

    void dropObjects()
    {
        //fixedJoint.breakForce = 1;
        //fixedJoint.currentForce = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY); 

        rb.AddForce(movePlayer * speed);
    }
}

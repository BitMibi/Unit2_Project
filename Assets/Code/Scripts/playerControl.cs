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
    private bool inRange = false;
    public GameObject liftable;
    //Empty objects to keep the liftable in place
    public GameObject holdPosition;
    public GameObject dropPosition;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    //Used to get move inputs
    void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();

        movementX = movement.x;
        movementY = movement.y;

    }

    void OnTriggerEnter(Collider liftable)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider liftable)
    {
        inRange = false;
    }

    //Used to check for Interact input
    void OnInteract()
    {
        if (!holding && liftable.CompareTag("PickUpTag") && inRange)
        {
            holding = true;
           
        }
        else if (holding)
        {
            holding = false;
            dropObjects();
        }
    }

    //Used to pick up objects to the top of the players head
    void pickUpObjects()
    {
        liftable.transform.position = holdPosition.transform.position;
    }

    
    //Used to drop objects in front of player
    void dropObjects()
    {
        liftable.transform.position = dropPosition.transform.position;
    }

    

    void Update(){
       
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY); 

        rb.AddForce(movePlayer * speed);

        //To move objects
        if (holding)
        {
            pickUpObjects();
        }
    }

}

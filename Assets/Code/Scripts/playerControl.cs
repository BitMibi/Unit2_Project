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
    private float pickUpRange = 30;
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

    //USed to check if player is within pick up range
    bool GetIfInRange(){
        if (transform.position.x <= liftable.transform.position.x + pickUpRange){
            if (transform.position.y <= liftable.transform.position.y + pickUpRange){
                if (transform.position.z <= liftable.transform.position.z + pickUpRange){
                    inRange = true;
                }
            }
        }
        
        return inRange;
    

    }

    //Used to check for Interact input
    void OnInteract()
    {
        if (!holding && liftable.CompareTag("PickUpTag") && GetIfInRange())
        {
            holding = true;
           
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
        liftable.transform.position = holdPosition.transform.position;
    }

    
    //Used to drop objects in front of player
    void dropObjects()
    {
        liftable.transform.position = dropPosition.transform.position;
    }

    

    void Update(){
        //To move objects
        if (holding)
        {
            pickUpObjects();
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY); 

        rb.AddForce(movePlayer * speed);

        
    }

}

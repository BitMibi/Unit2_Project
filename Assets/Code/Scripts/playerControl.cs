using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class playerControl : MonoBehaviour
{
    private Rigidbody rb;
   
    //Movement Variables
    private float movementX;
    private float movementY;
    public float speed;     //Speed used on ground
    public float airSpeed; //Speed used in air

    public float jumpForce; // Force used when jumping
    public GameObject GroundPlane;
    private bool isGrounded; //Checks if grounded
    //For relative to camera -- from 'https://www.youtube.com/watch?v=LaO1GDf2v3c'
    public CameraControl cameraControl;


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
        

        //*Change input based on camera -- from 'https://www.reddit.com/r/Unity3D/comments/eklm3r/how_to_move_player_relative_to_camera/'
        
        movementX = movement.x;
        movementY = movement.y;

        movementX *=  cameraControl.currentPosition.transform.right.x; 
        if (movementX != 0) //Correction of speed to 1
        {
            if (movementX < 0)
            {
                movementX = -1;
            }
            else if (movementX > 0)
            {
                movementX = 1;
            }
        }

        movementY *=  cameraControl.currentPosition.transform.right.x;
        if (movementY != 0) //Correction of speed to 1
        {
            if (movementY < 0)
            {
                movementY = -1;
            }
            else if (movementY > 0)
            {
                movementY = 1;
            }
        }
    }


    //Used to Jump
    private void OnCollisionEnter(Collision GroundPlane)
    {
        isGrounded = true;
    }
    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
        isGrounded = false;
    }

    //Checks if player object is in range to pick up objects
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
        liftable.transform.rotation = holdPosition.transform.rotation;
    }

    
    //Used to drop objects in front of player
    void dropObjects()
    {
        liftable.transform.position = dropPosition.transform.position;
    }

    void rotatePlayer()
    {
        if (movementX != 0 || movementY != 0)
        {
            //Definitely NOT taken from 'CMP112 Week 04 Lab -- Unity Introduction'
            float angle = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg;
            Vector3 rotation = new Vector3(0, angle, 0); 
            transform.eulerAngles = rotation;
        }
    }

    


    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY);
        //If grounded, move at max speed. Otherwise move slightly
        if (isGrounded)
        {
            rb.AddForce(movePlayer * speed);
        }
        else if (!isGrounded)
        {
            rb.AddForce(movePlayer * airSpeed);
        }

            //Rotate Player Object
            rotatePlayer();

        //To move objects
        if (holding)
        {
            pickUpObjects();
        }
    }

}

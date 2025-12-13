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
        

        //*Change input based on camera -- from https://www.reddit.com/r/Unity3D/comments/eklm3r/how_to_move_player_relative_to_camera/
        
        movementX = movement.x;
        movementY = movement.y;

        movementX *=  Camera.current.transform.right.x; //FIX IN THE MORNIHNG
        movementY *=  Camera.current.transform.up.z;

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
            float angle = Mathf.Atan2(movementX, movementY) * Mathf.Rad2Deg;
            Vector3 rotation = new Vector3(0, angle, 0); //Rotate around the z-axis
            transform.eulerAngles = rotation;
        }
    }

   

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movePlayer * speed);

        //Rotate Player Object
        rotatePlayer();

        //To move objects
        if (holding)
        {
            pickUpObjects();
        }
    }

}

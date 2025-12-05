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
    bool holding = false;
    public GameObject pickupObjects;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movement = movementValue.Get<Vector2>();

        movementX = movement.x;
        movementY = movement.y;

    }

    void OnInteract()
    {
        if (!holding)
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

    }

    void dropObjects()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePlayer = new Vector3(movementX, 0.0f, movementY); 

        rb.AddForce(movePlayer * speed);
    }
}

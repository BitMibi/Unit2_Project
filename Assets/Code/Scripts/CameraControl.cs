using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


public class CameraControl : MonoBehaviour
{
    //List of cameras in current scene
    public Camera[] room0Cameras;    //List of cameras in specific rooms
    public Camera[] room1Cameras;   
    public Camera[] room2Cameras;   //Max 3 rooms per scene 
    public int roomID = 0;  //Which room to switch to -- Public for roomChanger.cs
    private Camera[] currentRoom; //Stores which room player is currently in
    
    


    private Camera previousPosition;
    public Camera currentPosition;      //Public to be used to playerContol.cs
    private Camera nextPosition;

    private int previousJump;
    private int nextJump;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRoom = room0Cameras;

        

        

        //Create a position for previous and next to jump to
        previousJump = currentRoom.Length - 1;
        nextJump = 1;

        //Set the Positions to the objects in currentRoom   
        previousPosition = currentRoom[previousJump];
        currentPosition = currentRoom[0];
        nextPosition = currentRoom[nextJump];

        //Change which cameras are on -- 'https://discussions.unity.com/t/changing-between-cameras/3254'
        previousPosition.enabled = false;
        currentPosition.enabled = true;
        nextPosition.enabled = false;



    }

    void OnPrevious()
    {
        //Decrease previousJump
        if (previousJump > 0)
        {
            previousJump--;
        }
        else { //To avoid going outside array range
            previousJump = currentRoom.Length - 1;
        }

        //Switch to previous Camera
        currentPosition.enabled = false;
        previousPosition.enabled = true;

        //Change camera positions
        nextPosition = currentPosition;
        currentPosition = previousPosition;
        previousPosition = currentRoom[previousJump];

    }

    void OnNext()
    {
        //Increase nextJump
        if (nextJump < (currentRoom.Length - 1))
        {
            nextJump++;
        }
        else
        { //To avoid going outside array range
            nextJump = 0;
        }

        //Switch to next camera
        currentPosition.enabled = false;
        nextPosition.enabled = true;

        //Change the camera positions
        previousPosition = currentPosition;
        currentPosition = nextPosition;
        nextPosition = currentRoom[nextJump];
        
    }

    public void RoomChanged()
    {

        switch (roomID)
        {
            case 0: currentRoom = room0Cameras; break;
            case 1: currentRoom = room1Cameras; break;
            case 2: currentRoom = room2Cameras; break;
            default: break;
        }
        //Create a position for previous and next to jump to
        previousJump = currentRoom.Length - 1;
        nextJump = 1;

        //Set the Positions to the objects in currentRoom   
        previousPosition = currentRoom[previousJump];
        currentPosition = currentRoom[0];
        nextPosition = currentRoom[nextJump];

        //Change which cameras are on -- 'https://discussions.unity.com/t/changing-between-cameras/3254'
        previousPosition.enabled = false;
        currentPosition.enabled = true;
        nextPosition.enabled = false;
    }

    
}

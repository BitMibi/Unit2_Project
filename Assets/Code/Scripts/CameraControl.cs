using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


public class CameraControl : MonoBehaviour
{
    //List of cameras in current scene
    public Camera[] cameraList;
    
    


    private Camera previousPosition;
    public Camera currentPosition;      //Public to be used to playerContol.cs
    private Camera nextPosition;

    private int previousJump;
    private int nextJump;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Create a position for previous and next to jump to
        previousJump = cameraList.Length - 1;
        nextJump = 1;

        //Set the Positions to the objects in cameraList   
        previousPosition = cameraList[previousJump];
        currentPosition = cameraList[0];
        nextPosition = cameraList[nextJump];

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
            previousJump = cameraList.Length - 1;
        }

        //Switch to previous Camera
        currentPosition.enabled = false;
        previousPosition.enabled = true;

        //Change camera positions
        nextPosition = currentPosition;
        currentPosition = previousPosition;
        previousPosition = cameraList[previousJump];

    }

    void OnNext()
    {
        //Increase nextJump
        if (nextJump < (cameraList.Length - 1))
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
        nextPosition = cameraList[nextJump];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

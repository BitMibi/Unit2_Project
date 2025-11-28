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
    private Camera currentPosition;
    private Camera nextPosition;

    private int previousJump;
    private int nextJump;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Create a position for previous and next to jump to
        previousJump = cameraList.Length;
        nextJump = 1;

        //Set the Positions to the objects in cameraList   
        previousPosition = cameraList[previousJump];
        currentPosition = cameraList[0];
        nextPosition = cameraList[nextJump];

        //Change which cameras are on
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
            previousJump = cameraList.Length;
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
        //Decrease previousJump
        if (previousJump < cameraList.Length)
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

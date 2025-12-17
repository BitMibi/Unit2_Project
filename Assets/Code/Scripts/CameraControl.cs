using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;


public class CameraControl : MonoBehaviour
{
    //List of cameras in current scene
    public Camera[] room0Cameras;    //List of cameras in specific rooms
    public Camera[] room1Cameras;   
    public Camera[] room2Cameras;   //Max 3 rooms per scene 

    //Audio Listeners attached to the cameras
    public AudioListener[] room0Listeners;
    public AudioListener[] room1Listeners;
    public AudioListener[] room2Listeners;

    public int roomID = 0;  //Which room to switch to -- Public for roomChanger.cs
    private Camera[] currentRoomCameras; //Stores which cameras are in the room player is currently in
    private AudioListener[] currentRoomListeners; //Stores which audio listeners are in the room the player is currently in
    
    

    private Camera previousPosition;
    public Camera currentPosition;      //Public to be used to playerContol.cs
    private Camera nextPosition;

    private AudioListener previousAudio;
    private AudioListener currentAudio;
    private AudioListener nextAudio;

    private int previousJump;
    private int nextJump;       //Positions for previous and next to jump to if need be

    private bool destroyHit;    //Boolean to remake arrays when cameras are destroyed



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRoomCameras = room0Cameras;
        currentRoomListeners = room0Listeners;

        
        

        //Create a position for previous and next to jump to
        previousJump = currentRoomCameras.Length - 1;
        if (currentRoomCameras.Length == 1)
        {
            nextJump = 0;
        }
        else
        {
            nextJump = 1;
        }
        //Set the Positions to the objects in currentRoom   
        previousPosition = currentRoomCameras[previousJump];
        currentPosition = currentRoomCameras[0];
        nextPosition = currentRoomCameras[nextJump];

        //Same for audio listeners
        previousAudio = currentRoomListeners[previousJump];
        currentAudio = currentRoomListeners[0];
        nextAudio = currentRoomListeners[nextJump];

        //Change which cameras are on -- 'https://discussions.unity.com/t/changing-between-cameras/3254'

        currentPosition.enabled = true; 
        
        //Change which listeners are listening

        currentAudio.enabled = true; 
        
    }

    void OnPrevious()
    {
        //Decrease previousJump
        if (previousJump > 0)
        {
            previousJump--;
        }
        else { //To avoid going outside array range
            previousJump = currentRoomCameras.Length - 1;
        }

        //Switch to previous Camera
        currentPosition.enabled = false;
        previousPosition.enabled = true;

        //Switch to previous audio
        currentAudio.enabled = false;
        previousAudio.enabled = true;

        //Change camera positions
        nextPosition = currentPosition;
        currentPosition = previousPosition;
        previousPosition = currentRoomCameras[previousJump];

        //Change audio positions
        nextAudio = currentAudio;
        currentAudio = previousAudio;
        previousAudio = currentRoomListeners[previousJump];

    }

    void OnNext()
    {
        //Increase nextJump
        if (nextJump < (currentRoomCameras.Length - 1))
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

        //Switch to next listener
        currentAudio.enabled = false;
        nextAudio.enabled = true;

        //Change the camera positions
        previousPosition = currentPosition;
        currentPosition = nextPosition;
        nextPosition = currentRoomCameras[nextJump];

        //Change audio positions
        previousAudio = currentAudio;
        currentAudio = nextAudio;
        nextAudio = currentRoomListeners[nextJump];
    }

    public void RoomChanged()
    {
        currentPosition.enabled = false; //turn off old camera
        currentAudio.enabled = false; //turn off old listener

        switch (roomID)     //Change array to current room
        {
            case 0: currentRoomCameras = room0Cameras; currentRoomListeners = room0Listeners; break;
            case 1: currentRoomCameras = room1Cameras; currentRoomListeners = room1Listeners; break;
            case 2: currentRoomCameras = room2Cameras; currentRoomListeners = room2Listeners;  break;
            default: break;
        }

        //Create a position for previous and next to jump to
        previousJump = currentRoomCameras.Length - 1;
        if (currentRoomCameras.Length == 1)
        {
            nextJump = 0;
        }
        else
        {
            nextJump = 1;
        }

        //Set the Positions to the objects in currentRoom   
        previousPosition = currentRoomCameras[previousJump];
        currentPosition = currentRoomCameras[0];
        nextPosition = currentRoomCameras[nextJump];

        //Same for audio listeners
        previousAudio = currentRoomListeners[previousJump];
        currentAudio = currentRoomListeners[0];
        nextAudio = currentRoomListeners[nextJump];

        //Change which cameras are on -- 'https://discussions.unity.com/t/changing-between-cameras/3254'

        
        currentPosition.enabled = true;
        

        //Change which listeners are listening

        
        currentAudio.enabled = true;
        

    }

    public void remakeArray(int cameraDestroyed)
    {
        destroyHit = false;

        //Switch to next camera
        currentPosition.enabled = false;
        nextPosition.enabled = true;

        //Switch to next listener
        currentAudio.enabled = false;
        nextAudio.enabled = true;


        //Remake current array without selected camera
        for (int i = 0; i < currentRoomCameras.Length; i++)
        {
            if (i != cameraDestroyed && !destroyHit)
            {
                currentRoomCameras[i] = currentRoomCameras[i];
                currentRoomListeners[i] = currentRoomListeners[i];
            }
            else if (i == cameraDestroyed)
            {
                destroyHit = true;

            }
            else
            {
                currentRoomCameras[i - 1] = currentRoomCameras[i];  //Removes selected camera from array
                currentRoomListeners[i - 1] = currentRoomListeners[i]; //removes selected audio from array
            }
             
        }

        Array.Resize(ref currentRoomCameras, currentRoomCameras.Length - 1);        //From "https://discussions.unity.com/t/resize-an-array-in-c/430138/3"
        Array.Resize(ref currentRoomListeners, currentRoomListeners.Length - 1);

        //Create a position for previous and next to jump to
        previousJump = currentRoomCameras.Length - 1;
        if (currentRoomCameras.Length == 1)
        {
            nextJump = 0;
        }
        else
        {
            nextJump = 1;
        }

        //Change the camera positions
        previousPosition = currentRoomCameras[previousJump];
        currentPosition = currentRoomCameras[0];
        nextPosition = currentRoomCameras[nextJump];

        //Change audio positions
        previousAudio = currentRoomListeners[previousJump];
        currentAudio = currentRoomListeners[0];
        nextAudio = currentRoomListeners[nextJump];
    }

}

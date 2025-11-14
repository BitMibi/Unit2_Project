using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;


public class CameraControl : MonoBehaviour
{
    //Preset poisitons for the camera to point   
    public Vector3 position1;       
    public Vector3 position2;
    public Vector3 position3;
    public Vector3 position4;
    public Vector3 position5;


    private float directionX;
    private float directionY;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = position1;
    }

    void OnLook()
    {
        

        switch (transform.position) {           //Attempting a switch case to copy across right and left inputs to switch between the camera positons. idk how i'll figure it out copium
            case Vector3(position1): transform.position = position2;         break;     

        
    } 


    // Update is called once per frame
    void Update()
    {
         
    }
}

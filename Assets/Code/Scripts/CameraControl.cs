using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;


public class CameraControl : MonoBehaviour
{
    //Preset poisitons for the camera to point   
    public GameObject cameraModel1;
    public GameObject cameraModel2;
    //Ad infinitum for cameras in level


    private float directionX;
    private float directionY;

    private GameObject previousPosition;
    private GameObject nextPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = cameraModel1.transform.position;
    }

    void OnLook()
    {


        //Make it cycle between camera objects


    }


    // Update is called once per frame
    void Update()
    {

    }
}

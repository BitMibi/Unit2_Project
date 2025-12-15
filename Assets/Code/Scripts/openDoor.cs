using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


public class openDoor : MonoBehaviour
{

    private Rigidbody rb;

    //Booleans to control movement
    private bool opening = false; // To trigger the opening of the door
    private bool closing = false; // To trigger the closing of the door
    private bool isOpen = false; //Checks if door is open or not
    public GameObject openPos;
    public GameObject closePos; 

    //Button Checks
    public buttonCheck[] buttonsRequired; // Which buttons are required to be down to open the door
    private bool allButtonsDown = false;    //When all buttons are down, start open script
    private int buttonCount = 0;    //Increases for amount of buttons active


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }


    void doorMovement()
    {
        if (opening && !isOpen)
        {
            StopCoroutine(Close());
            StartCoroutine(Open());
        }
        else if (closing && isOpen)
        {
            StopCoroutine(Open());
            StartCoroutine(Close());
        }
    }

    IEnumerator Open()
    {
        isOpen = true;
        opening = false; //Opens Once

        
        for (float i = rb.transform.position.x; i <= openPos.transform.position.x; i += 0.2f)
        {            
            rb.MovePosition(new Vector3(i, rb.transform.position.y, rb.transform.position.z));
            yield return new WaitForSeconds(0.1f);
        }


       
    }

    IEnumerator Close()
    {
        isOpen = false;
        closing = false;


        
        for (float i = rb.transform.position.x; i >= closePos.transform.position.x; i -= 0.2f)
        {
            rb.MovePosition(new Vector3(i, rb.transform.position.y, rb.transform.position.z));
            yield return new WaitForSeconds(0.1f);
        }

       
    }

    void buttonsCheck()
    {
       
        for (int i = 0; i < buttonsRequired.Length; i++)
        {            
            if (buttonsRequired[i].isDown && buttonCount < buttonsRequired.Length)      //Checks array for amount of buttons down, increases count if true and the count is less than length of array
            {
                buttonCount++;                
            }
            else if (!buttonsRequired[i].isDown && buttonCount <= buttonsRequired.Length)
            {
                buttonCount = 0;                                                          //Resets count if otherwise                 
            }
        }
        if (buttonCount == buttonsRequired.Length)
        {
            allButtonsDown = true;
        }
        else
        {
            allButtonsDown = false;
        }

    }


    // Update is called once per frame
    void Update()
    {
        buttonsCheck();
        
        if (allButtonsDown)
        {
            opening = true;
            doorMovement();
        }
        else
        {
            closing = true;
            doorMovement();
        }
    }
}

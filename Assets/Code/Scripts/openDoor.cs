using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


public class openDoor : MonoBehaviour
{

    private Rigidbody rb;


    private bool opening = false; // To trigger the opening of the door
    private bool closing = false; // To trigger the closing of the door
    private bool isOpen = false; //Checks if door is open or not

    public bool allButtonsDown = false;


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

        float endPos = rb.transform.position.x + 1.3f;
        for (float i = rb.transform.position.x; i <= endPos; i += 0.2f)
        {            
            rb.MovePosition(new Vector3(i, rb.transform.position.y, rb.transform.position.z));
            yield return new WaitForSeconds(0.1f);
        }


       
    }

    IEnumerator Close()
    {
        isOpen = false;
        closing = false;


        float endPos = rb.transform.position.x - 1.3f;
        for (float i = rb.transform.position.x; i >= endPos; i -= 0.2f)
        {
            rb.MovePosition(new Vector3(i, rb.transform.position.y, rb.transform.position.z));
            yield return new WaitForSeconds(0.1f);
        }

       
    }

    
    // Update is called once per frame
    void Update()
    {
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

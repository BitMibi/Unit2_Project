using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class buttonCheck : MonoBehaviour
{

    private Rigidbody rb;

    private int collisionsHappening = 0; // Bugfix for if an object leaves the button while something else is still on it it goes up
    public bool isDown = false; // Public for openDoor

    public float waitTime; 

    public GameObject upPos;
    public GameObject downPos; //Up and down positions to move to

    public bool heavyButton; //Bool to differentiate between heavy and light buttons

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Up()
    {
        

        
        for (float i = rb.transform.position.y; i <= upPos.transform.position.y; i += 0.01f)
        {
            rb.MovePosition(new Vector3(rb.transform.position.x, i, rb.transform.position.z));
            yield return new WaitForSeconds(waitTime);
        }



    }

    IEnumerator Down()
    {


        
        for (float i = rb.transform.position.y; i >= downPos.transform.position.y; i -= 0.01f)
        {
            rb.MovePosition(new Vector3(rb.transform.position.x, i, rb.transform.position.z));
            yield return new WaitForSeconds(waitTime);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        string output = $"My name is {name} and I have started a collision with {collision.gameObject.name}.";
        Debug.Log(output);
        if (heavyButton && collision.gameObject.name != "LightCube")     //Heavy buttons cannot be pushed down by light cubes.
        {
            collisionsHappening++;
            if (!isDown && collisionsHappening >= 1)
            {
                StartCoroutine(Down());
                isDown = true;
            }
        }
        else if (!heavyButton)          //Non-heavy buttons can be pushed down by any object
        {
            collisionsHappening++;
            if (!isDown && collisionsHappening >= 1)
            {
                StartCoroutine(Down());
                isDown = true;
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (heavyButton && collision.gameObject.name != "LightCube")
        {
            collisionsHappening--;
            if (isDown && collisionsHappening == 0)
            {
                StartCoroutine(Up());
                isDown = false;
            }
        }
        else if (!heavyButton)
        {
            collisionsHappening--;
            if (isDown && collisionsHappening == 0)
            {
                StartCoroutine(Up());
                isDown = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

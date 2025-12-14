using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

public class buttonCheck : MonoBehaviour
{

    private Rigidbody rb;

    private bool collisionHappening = false; // Bugfix for if an object leaves the button while something else is still on it it goes up
    private bool isDown = false; //Used to open door

    public GameObject upPos;
    public GameObject downPos; //Up and down positions to move to
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator Up()
    {
        

        
        for (float i = rb.transform.position.y; i <= upPos.transform.position.y; i += 0.01f)
        {
            rb.MovePosition(new Vector3(rb.transform.position.x, i, rb.transform.position.z));
            yield return new WaitForSeconds(0.2f);
        }



    }

    IEnumerator Down()
    {


        
        for (float i = rb.transform.position.y; i >= downPos.transform.position.y; i -= 0.01f)
        {
            rb.MovePosition(new Vector3(rb.transform.position.x, i, rb.transform.position.z));
            yield return new WaitForSeconds(0.2f);
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        collisionHappening = true;
        if (!isDown)
        {
            StartCoroutine(Down());
            isDown = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exit");
        if (isDown && !collisionHappening)
        {
            StartCoroutine(Up());
            isDown = false;
            collisionHappening = false;
        }
        
         
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

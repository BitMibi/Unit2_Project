using System;
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

    //For random button
    public bool randomButton;
    private float randomFloat;
    private AudioSource audioSource;
    public AudioClip buzzer;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
        if (!collision.gameObject.CompareTag("Button"))
        {
            if (heavyButton && collision.gameObject.transform.childCount == 0)     //Heavy buttons cannot be pushed down by light cubes. (Light cubes have exactly 1 child.)
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
                    if (randomButton)       //If button is random
                    {
                        randomFloat = UnityEngine.Random.value;
                        if (randomFloat >= 0.5)     //Coin flip for isDown
                        {
                            isDown = true;
                        }
                        else
                        {
                            audioSource.PlayOneShot(buzzer, 0.5f);
                        }
                    }
                    else        //If button is NOT random
                    {
                        isDown = true;      //Treat as usual
                    }
                }
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (heavyButton && collision.gameObject.transform.childCount == 0)
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
            if ((isDown || randomButton) && collisionsHappening == 0)
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

using System.Collections;
using UnityEngine;


public class openDoor : MonoBehaviour
{

    private Rigidbody rb;

    
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Open();
    }

    void Open()
    {
        for (float i = 0; i < rb.transform.position.x + 1; i += 0.1f)
        {

            rb.transform.position = new Vector3(rb.transform.position.x + i, 0, 0);
            StartCoroutine(AnimationTime());
        }
    }

    void Close()
    {
        for (float i = 0; i < rb.transform.position.x - 1; i -= 0.1f)
        {

            rb.transform.position = new Vector3(rb.transform.position.x - i, 0, 0);
            StartCoroutine(AnimationTime());
        }
    }

    IEnumerator AnimationTime()
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

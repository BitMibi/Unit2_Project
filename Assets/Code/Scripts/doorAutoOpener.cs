using UnityEngine;

public class doorAutoOpener : MonoBehaviour
{

    public bool beOpen;  //Public bool for if the trigger should open or close the attached door
    public openDoor openDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (beOpen)
            {
                openDoor.StartCoroutine(openDoor.Open());
            }
            else
            {
                openDoor.StartCoroutine(openDoor.Close());
                openDoor.buttonDoor = true; //shut door permanently 
            }
            Destroy(gameObject);
        }
    }
}

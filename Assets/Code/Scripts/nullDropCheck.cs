using UnityEngine;

public class nullDropCheck : MonoBehaviour
{
    public playerControl playerControl;

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("PickUpTag") && !other.gameObject.CompareTag("Button") && !other.gameObject.CompareTag("Trigger"))
        {
            playerControl.droppable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        playerControl.droppable = true;

    }
}

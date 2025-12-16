using UnityEngine;

public class nullDropCheck : MonoBehaviour
{
    public playerControl playerControl;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("PickUpTag"))
        {
            playerControl.droppable = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        playerControl.droppable = true;

    }
}

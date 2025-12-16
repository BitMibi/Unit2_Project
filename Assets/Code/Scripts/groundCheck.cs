using UnityEngine;

public class groundCheck : MonoBehaviour
{

    public playerControl playerControl;

    
    private void OnTriggerEnter(Collider boxCollider)
    {
        Debug.Log("Collided");
        playerControl.isGrounded = true;
    }

}

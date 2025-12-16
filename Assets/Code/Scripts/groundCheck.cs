using UnityEngine;

public class groundCheck : MonoBehaviour
{

    public playerControl playerControl;

    
    private void OnTriggerEnter(Collider boxCollider)
    {
        playerControl.isGrounded = true;
    }

}

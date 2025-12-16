using UnityEngine;

public class cameraDestroyer : MonoBehaviour
{
    public int cameraDestroyed;  //Integer for which camera to be destroyed
    public CameraControl CameraControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CameraControl.remakeArray(cameraDestroyed);
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class roomChanger : MonoBehaviour
{

    public int roomChange;  //Integer for which room to change to on trigger
    public CameraControl CameraControl;

    private void OnTriggerEnter(Collider other)
    {
        CameraControl.roomID = roomChange;
        CameraControl.RoomChanged();
        Destroy(gameObject);
    }
}

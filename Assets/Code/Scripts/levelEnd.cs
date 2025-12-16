using UnityEngine;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour
{
    public string sceneToLoad;      //Change in inspector which scene this trigger loads
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))      //If player collides load the next scene
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

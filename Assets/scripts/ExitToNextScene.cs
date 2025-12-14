using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToNextScene : MonoBehaviour
{
    bool exiting = false;

    void OnTriggerEnter(Collider other)
    {
        if (exiting) return;
        exiting = true;

        Debug.Log("EXIT ZONE ENTERED");

        Invoke(nameof(ExitScene), 1.5f);
    }

    void ExitScene()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }
}

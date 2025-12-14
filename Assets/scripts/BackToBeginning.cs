using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToBeginning : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    [Header("Credits")]
    public RectTransform creditsText;
    public float scrollSpeed = 50f;

    [Header("End Button")]
    public GameObject homeButton;
    public float showButtonAtY = 1200f;

    private bool buttonShown = false;

    void Start()
    {
        if (homeButton != null)
            homeButton.SetActive(false);
    }

    void Update()
    {
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (!buttonShown && creditsText.anchoredPosition.y >= showButtonAtY)
        {
            buttonShown = true;
            homeButton.SetActive(true);
        }
    }

    // Hook this to the button
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("start");
    }
}

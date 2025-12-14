using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;   // NEW

public class BouncerTalk2 : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup dialogueCanvasGroup;  
    public TMP_Text npcText;                 
    public Button playerButton;              
    public TMP_Text playerButtonText;        

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.04f;        

    [Header("Character Animation")]
    public Animator npcAnimator;             
    public string talkTriggerName = "Talk";  

    [Header("Dialogue Content")]
    [TextArea(3,10)]
    public string[] npcLines;

    [TextArea(2,4)]
    public string[] playerResponses;

    [Header("Scene Transition")]
    public string nextSceneName = "credits"; // NEW — assign in Inspector
    public float fadeOutDuration = 1.2f;    // NEW

    private int currentLine = 0;
    private bool isTyping = false;

    void Start()
    {
        dialogueCanvasGroup.alpha = 1f;
        dialogueCanvasGroup.interactable = true;
        dialogueCanvasGroup.blocksRaycasts = true;

        npcText.text = "...";  
        
        if (playerResponses.Length > 0)
            playerButtonText.text = playerResponses[0];

        playerButton.onClick.AddListener(OnPlayerButtonPressed);
    }

    void OnPlayerButtonPressed()
    {
        if (isTyping) return;

        if (currentLine < npcLines.Length)
        {
            StartCoroutine(TypeLine(npcLines[currentLine]));

            if (npcAnimator != null)
                npcAnimator.SetTrigger(talkTriggerName);

            if (currentLine + 1 < playerResponses.Length)
                playerButtonText.text = playerResponses[currentLine + 1];

            currentLine++;

            // NEW — if this was the LAST line, disable further input
            if (currentLine >= npcLines.Length)
                playerButtonText.text = "";  
        }

        playerButton.interactable = false;
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        npcText.text = "";

        foreach (char c in line)
        {
            npcText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        playerButton.interactable = true;

        // NEW — if we've reached the end, fade out UI + change scene
        if (currentLine >= npcLines.Length)
        {
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    // NEW — UI fade-out and scene transition
    IEnumerator FadeOutAndChangeScene()
    {
        // Disable clicking
        playerButton.interactable = false;
        dialogueCanvasGroup.interactable = false;
        dialogueCanvasGroup.blocksRaycasts = false;

        float t = 0f;
        float startAlpha = dialogueCanvasGroup.alpha;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, t / fadeOutDuration);
            dialogueCanvasGroup.alpha = alpha;
            yield return null;
        }

        // Optional delay before switching
        yield return new WaitForSeconds(0.5f);

        // Load next scene
        SceneManager.LoadScene(4);
    }
}
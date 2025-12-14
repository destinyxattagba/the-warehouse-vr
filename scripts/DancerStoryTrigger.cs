

using System.Collections;
using UnityEngine;
using TMPro;

public class DancerStoryTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    public TMP_Text dialogueText;
    public CanvasGroup dialogueGroup;

    [TextArea(2, 6)]
    public string[] lines;

    public float typingSpeed = 0.04f;
    public float delayBetweenLines = 3f;

    [Header("Animation")]
    public Animator animator;
    public string danceBool = "IsDancing";
    public string backflipTrigger = "DoBackflip";

    private bool playerInside = false;
    private Coroutine talkRoutine;

    void Start()
    {
        animator.SetBool(danceBool, true);

        dialogueGroup.alpha = 0;
        dialogueGroup.interactable = false;
        dialogueGroup.blocksRaycasts = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player")) return;
        if (playerInside) return;

        playerInside = true;

        dialogueGroup.alpha = 1;
        dialogueGroup.interactable = true;
        dialogueGroup.blocksRaycasts = true;

        talkRoutine = StartCoroutine(StoryRoutine());
    }

    void OnTriggerExit(Collider other)
    {

        if (!other.CompareTag("Player")) return;

        playerInside = false;

        if (talkRoutine != null)
            StopCoroutine(talkRoutine);

        dialogueGroup.alpha = 0;

        animator.SetBool(danceBool, true);
    }

    IEnumerator StoryRoutine()
    {
        animator.SetBool(danceBool, false);

        dialogueGroup.alpha = 1;

        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(delayBetweenLines);
        }

        // Backflip moment
        animator.SetTrigger(backflipTrigger);

        yield return new WaitForSeconds(2.0f); // match backflip length

        dialogueGroup.alpha = 0;
        dialogueGroup.interactable = false;
        dialogueGroup.blocksRaycasts = false;

        if (playerInside)
            animator.SetBool(danceBool, true);
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}

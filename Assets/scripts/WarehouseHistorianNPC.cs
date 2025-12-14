using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WarehouseHistorianNPC : MonoBehaviour
{
    [Header("Dialogue UI")]
    public CanvasGroup dialogueGroup;
    public TMP_Text npcText;
    public Button okButton;
    public TMP_Text okButtonText;

    [Header("Dialogue Content")]
    [TextArea(2, 6)]
    public string[] lines;

    public float typingSpeed = 0.04f;
    public float delayBetweenLines = 1.2f;

    [Header("Optional Animation")]
    public Animator animator;
    public string walkBool = "isWalking";

    private bool hasSpoken = false;
    private bool playerInside = false;

    void Start()
    {
        if (animator != null)
        animator.SetBool(walkBool, true);

        dialogueGroup.alpha = 0;
        dialogueGroup.interactable = false;
        dialogueGroup.blocksRaycasts = false;

        okButton.gameObject.SetActive(false);
        okButtonText.text = "Thank you, I will!";

        okButton.onClick.AddListener(OnOkPressed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (animator != null)
        animator.SetBool(walkBool, false);

        if (!other.CompareTag("Player")) return;
        if (hasSpoken) return;

        playerInside = true;
        hasSpoken = true;

        ShowDialogue(true);
        StartCoroutine(DialogueRoutine());
    }

    void OnTriggerExit(Collider other)
    {
        if (animator != null)
        animator.SetBool(walkBool, true);

        if (!other.CompareTag("Player")) return;
        playerInside = false;
    }

    IEnumerator DialogueRoutine()
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(delayBetweenLines);
        }

        // Final redirect line

        yield return StartCoroutine(TypeLine("You should go say hi to Frankie."));

        okButton.gameObject.SetActive(true);
    }

    IEnumerator TypeLine(string line)
    {
        npcText.text = "";

        foreach (char c in line)
        {
            npcText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void OnOkPressed()
    {
        ShowDialogue(false);
    }

    void ShowDialogue(bool show)
    {
        dialogueGroup.alpha = show ? 1 : 0;
        dialogueGroup.interactable = show;
        dialogueGroup.blocksRaycasts = show;
    }

}

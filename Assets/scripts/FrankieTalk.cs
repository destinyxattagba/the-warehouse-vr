using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrankieTalk : MonoBehaviour
{
    [Header("Dialogue UI")]
    public CanvasGroup dialogueGroup;
    public TMP_Text npcText;
    public Button playerButton;
    public TMP_Text playerButtonText;

    [Header("Vinyl Options")]
    public GameObject vinylA;
    public GameObject vinylB;

    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip songA;
    public AudioClip songB;

    [Header("Animation")]
    public Animator frankieAnimator;
    public string cheerBool = "IsCheering";
    public string talkBool = "IsTalking";
    public string waveTrigger = "Wave";

    private bool hasStarted = false;
    private int dialogueStep = 0;

    void Start()
    {
        ShowDialogue(false);

        vinylA.SetActive(false);
        vinylB.SetActive(false);

        playerButton.onClick.AddListener(OnPlayerResponse);

        // Frankie starts vibing
        if (frankieAnimator != null)
        {
            frankieAnimator.SetBool(cheerBool, true);
            frankieAnimator.SetBool(talkBool, false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasStarted) return;
        if (!other.CompareTag("Player")) return;

        hasStarted = true;
        StartConversation();
    }

    void StartConversation()
    {
        if (frankieAnimator != null)
        {
            frankieAnimator.SetBool(cheerBool, false);
            frankieAnimator.SetTrigger(waveTrigger);
            frankieAnimator.SetBool(talkBool, true);
        }

        ShowDialogue(true);

        dialogueStep = 0;
        npcText.text = "Hey—are you enjoying the music?";
        playerButtonText.text = "Yeah, this is my first time here and I love it.";
        playerButton.gameObject.SetActive(true);
    }

    void OnPlayerResponse()
    {
        if (dialogueStep == 0)
        {
            npcText.text = "Glad to hear it. Im Frankie Knuckles, resident DJ of the Warehouse";
            playerButtonText.text = "Honor to meet you!.";
            dialogueStep++;
        }
        else if (dialogueStep == 1)
        {
            npcText.text = "The pleasure is all mine. Tell you what—go ahead and pick the next record.";
            playerButtonText.text = "Pick a record";
            dialogueStep++;
        }
        else if (dialogueStep == 2)
        {
            playerButton.gameObject.SetActive(false);
            Invoke(nameof(ShowVinylChoices), 0.5f);
        }
    }

    void ShowVinylChoices()
    {
        ShowDialogue(false);

        vinylA.SetActive(true);
        vinylB.SetActive(true);
    }

    public void PickSongA()
    {
        SwitchSong(songA);
    }

    public void PickSongB()
    {
        SwitchSong(songB);
    }

    void SwitchSong(AudioClip clip)
    {
        vinylA.SetActive(false);
        vinylB.SetActive(false);

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();

        // Frankie goes back to vibing
        if (frankieAnimator != null)
        {
            frankieAnimator.SetBool(talkBool, false);
            frankieAnimator.SetBool(cheerBool, true);
        }
    }

    void ShowDialogue(bool show)
    {
        dialogueGroup.alpha = show ? 1 : 0;
        dialogueGroup.interactable = show;
        dialogueGroup.blocksRaycasts = show;
    }
}

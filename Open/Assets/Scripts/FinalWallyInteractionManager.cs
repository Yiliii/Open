using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalWallyInteractionManager : MonoBehaviour
{
    public float killDelayTime = 10f;

    [Header("Emotes")]
    public GameObject emoteWally;   // Emote shown above Wally
    public GameObject emotePlayer;  // Emote shown above Player

    [Header("Dialogue Intro")]
    public GameObject blackScreen;
    public GameObject dialogBox;
    public TMP_Text dialogText;

    private string[] introDialogues = new string[]
    {
        "Wally: Minty?",
        "Wally: Minty?!",
        "(Wally is back home now!)",
        "(What do I do now?)",
        "(I walked to the front door in the kitchen... and Wally is here)",
        "Wally: Why are you out?",
        "Wally: Where is Minty?!",
        "(Wally sees the bloodstain on my sleeve)",
        "Wally: What did you do to Minty?!",
        "Wally: I asked you — what did you do to your sister?! You monster!"
    };

    private int dialogueIndex = 0;
    private bool isIntroPlaying = true;
    private float autoAdvanceDelay = 2.5f;
    private float advanceTimer = 0f;

    private bool playerInRange = false;
    private bool hasUsedItem = false;
    private float timer = 0f;

    private GameObject player;
    private InventoryUIController inventoryUI;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryUI = FindObjectOfType<InventoryUIController>();

        HideAllEmotes();

        if (blackScreen != null) blackScreen.SetActive(true);
        if (dialogBox != null && dialogText != null)
        {
            dialogBox.SetActive(true);
            dialogText.text = introDialogues[dialogueIndex];
        }
    }

    void Update()
    {
        if (isIntroPlaying)
        {
            advanceTimer += Time.deltaTime;
            if (advanceTimer >= autoAdvanceDelay)
            {
                dialogueIndex++;
                advanceTimer = 0f;

                if (dialogueIndex < introDialogues.Length)
                {
                    dialogText.text = introDialogues[dialogueIndex];

                    if (dialogueIndex == 5 && blackScreen != null)
                        blackScreen.SetActive(false);
                }
                else
                {
                    isIntroPlaying = false;
                    if (dialogBox != null) dialogBox.SetActive(false);
                    timer = 0f;
                }
            }
            return;
        }

        if (inventoryUI != null && inventoryUI.IsInventoryOpen()) 
        {
            HideAllEmotes();
            return;
        }

        ShowRelevantEmote();

        if (!hasUsedItem)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= killDelayTime)
            {
                Debug.Log("Yes");
                SceneManager.LoadScene("Killed_Ending");
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            CollectableItem held = InventoryManager.Instance.itemInHand;
            if (held == null) return;

            if (held.itemName == "Knife")
            {
                if (playerInRange)
                {
                    GameStateManager.Instance.wallyDead = true;
                    SceneManager.LoadScene("Wally_death_Ending");
                }
                else
                {
                    SceneManager.LoadScene("suicide_ending");
                }
            }
            else if (held.itemName == "FamilyPhoto" && playerInRange)
            {
                SceneManager.LoadScene("Prison_Ending");
            }
        }
    }

    void ShowRelevantEmote()
    {
        CollectableItem held = InventoryManager.Instance.itemInHand;

        if (held == null)
        {
            HideAllEmotes();
            return;
        }

        if (held.itemName == "Knife")
        {
            if (playerInRange)
            {
                if (emoteWally) emoteWally.SetActive(true);
                if (emotePlayer) emotePlayer.SetActive(false);
            }
            else
            {
                if (emoteWally) emoteWally.SetActive(false);
                if (emotePlayer) emotePlayer.SetActive(true);
            }
        }
        else if (held.itemName == "FamilyPhoto")
        {
            if (emoteWally) emoteWally.SetActive(true);
            if (emotePlayer) emotePlayer.SetActive(false);
        }
        else
        {
            HideAllEmotes();
        }
    }

    void HideAllEmotes()
    {
        if (emoteWally) emoteWally.SetActive(false);
        if (emotePlayer) emotePlayer.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            timer = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
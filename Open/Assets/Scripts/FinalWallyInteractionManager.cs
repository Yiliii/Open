using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalWallyInteractionManager : MonoBehaviour
{
    public float killDelayTime = 10f;

    [Header("Emotes")]
    public GameObject emoteWally;   // Emote shown above Wally
    public GameObject emotePlayer;  // Emote shown above Player

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
    }

    void Update()
    {
        if (hasUsedItem) return;
        if (inventoryUI != null && inventoryUI.IsInventoryOpen()) 
        {
            HideAllEmotes();
            return;
        }

        ShowRelevantEmote();

        // if (playerInRange && !hasUsedItem)
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
                hasUsedItem = true;
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
                hasUsedItem = true;
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
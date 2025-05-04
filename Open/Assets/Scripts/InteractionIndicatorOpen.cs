using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionIndicatorOpen : MonoBehaviour
{
    [Header("Emoji References")]
    public GameObject regularEmoji; // for normal interactable
    public GameObject specialEmoji; // for collectable

    [Header("Settings")]
    public bool isCollectable = false;

    private bool playerInRange = false;
    private bool isInteracting = false;

    void Start()
    {
        HideAllEmojis();
    }

    void Update()
    {
        if (isInteracting)
        {
            HideAllEmojis();
            return;
        }

        if (playerInRange)
        {
            if (isCollectable)
                ShowSpecialEmoji();
            else
                ShowRegularEmoji();
        }
        else
        {
            HideAllEmojis();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void BeginInteraction()
    {
        isInteracting = true;
        HideAllEmojis();
    }

    public void EndInteraction()
    {
        isInteracting = false;
    }

    private void ShowRegularEmoji()
    {
        if (regularEmoji) regularEmoji.SetActive(true);
        if (specialEmoji) specialEmoji.SetActive(false);
    }

    private void ShowSpecialEmoji()
    {
        if (specialEmoji) specialEmoji.SetActive(true);
        if (regularEmoji) regularEmoji.SetActive(false);
    }

    private void HideAllEmojis()
    {
        if (regularEmoji) regularEmoji.SetActive(false);
        if (specialEmoji) specialEmoji.SetActive(false);
    }
}
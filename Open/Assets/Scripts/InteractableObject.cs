using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    [TextArea]
    public string interactionText;

    public GameObject dialogBox;
    public TMP_Text dialogText;

    private InventoryUIController inventoryUIController;
    private bool playerInRange = false;
    private bool dialogActive = false;

    private List<string> textSegments = new List<string>();
    private int currentSegmentIndex = 0;

    void Start()
    {
        inventoryUIController = FindObjectOfType<InventoryUIController>();
    }

    void Update()
    {
        if (inventoryUIController != null && inventoryUIController.IsInventoryOpen())
        {
            CloseDialog();
            return;
        }

        if (playerInRange)
        {
            if (!dialogActive && Input.GetKeyDown(KeyCode.E))
            {
                StartDialog();
            }
            else if (dialogActive && Input.GetKeyDown(KeyCode.Space))
            {
                NextSegment();
            }
        }
    }

    void StartDialog()
    {
        textSegments = new List<string>(interactionText.Split(new[] { "[next]" }, System.StringSplitOptions.None));
        currentSegmentIndex = 0;
        dialogBox.SetActive(true);
        dialogText.text = textSegments[currentSegmentIndex].Trim();
        dialogActive = true;
    }

    void NextSegment()
    {
        currentSegmentIndex++;
        if (currentSegmentIndex < textSegments.Count)
        {
            dialogText.text = textSegments[currentSegmentIndex].Trim();
        }
        else
        {
            CloseDialog();
        }
    }

    void CloseDialog()
    {
        dialogBox.SetActive(false);
        dialogActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            CloseDialog();
        }
    }
}
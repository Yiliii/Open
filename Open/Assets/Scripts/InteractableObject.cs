using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [TextArea]
    public string interactionText;

    private bool playerInRange = false;
    public GameObject dialogBox;
    public TMPro.TMP_Text dialogText;

    private InventoryUIController inventoryUIController;

    void Start()
    {
        inventoryUIController = FindObjectOfType<InventoryUIController>();
                
    }


    void Update()
    {
        if (inventoryUIController != null && inventoryUIController.IsInventoryOpen())
        {
            dialogBox.SetActive(false);
        }
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = interactionText;
            }
        }
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
            if (dialogBox.activeInHierarchy)
                dialogBox.SetActive(false);
        }
    }
}

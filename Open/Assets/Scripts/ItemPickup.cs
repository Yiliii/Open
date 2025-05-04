using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public CollectableItem item;
    public string uniqueID;  // A unique ID like "Knife_Kitchen" or "Photo_Storage"

    private bool canInteract = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D pickupCollider;
    public GameObject dialogBox; // Added for showing pickup message
    public TMPro.TMP_Text dialogText;
    public float messageDuration = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();

        // If item already collected, disable the object permanently
        if (InventoryManager.Instance.HasItem(uniqueID))
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (!InventoryManager.Instance.HasItem(uniqueID))
            {
                InventoryManager.Instance.AddItem(item, uniqueID);
                ShowPickupMessage($"Collected a {item.itemName}... (Press Z to View)");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canInteract = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            canInteract = false;
    }

    void ShowPickupMessage(string message)
    {
        if (dialogBox != null && dialogText != null)
        {
            StartCoroutine(DisplayMessageCoroutine(message));
        }
    }

    IEnumerator DisplayMessageCoroutine(string message)
    {
        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        dialogBox.SetActive(true);
        dialogText.text = message;

        yield return new WaitForSeconds(messageDuration);
        Debug.Log("After 2sec");
        dialogBox.SetActive(false);
        gameObject.SetActive(false); // disappear forever
    }
}

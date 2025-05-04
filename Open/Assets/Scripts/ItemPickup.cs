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
                gameObject.SetActive(false); // disappear forever
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
}

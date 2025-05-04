using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<CollectableItem> collectedItems = new();
    public CollectableItem itemInHand = null;

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<string> collectedItemIDs = new();

    public void AddItem(CollectableItem item, string uniqueID)
    {
        if (!collectedItemIDs.Contains(uniqueID))
        {
            collectedItems.Add(item);
            collectedItemIDs.Add(uniqueID);
            onInventoryChangedCallback?.Invoke();
            Debug.Log("Collected: " + item.itemName);
        }
    }

    public bool HasItem(string uniqueID)
    {
        return collectedItemIDs.Contains(uniqueID);
    }

    public void HoldItem(CollectableItem item)
    {
        if (HasItem(item.itemName))
        {
            itemInHand = item;
            onInventoryChangedCallback?.Invoke();
            Debug.Log("Now holding: " + item.itemName);
        }
    }

    public void UseItemInFront(GameObject target)
    {
        if (itemInHand == null) return;

        if (target.name == "Wally")
        {
            if (itemInHand.itemName == "Knife")
            {
                GameStateManager.Instance.wallyDead = true;
                Debug.Log("You stabbed Wally.");
            }
            else if (itemInHand.itemName == "FamilyPhoto")
            {
                Debug.Log("You showed Wally the family photo.");
            }
        }
        else if (target.name == "Player" && itemInHand.itemName == "Knife")
        {
            Debug.Log("You harmed yourself.");
        }
    }

    public void ResetInventory()
    {
        collectedItems.Clear();
        collectedItemIDs.Clear();
        itemInHand = null;

        onInventoryChangedCallback?.Invoke();
    }
}

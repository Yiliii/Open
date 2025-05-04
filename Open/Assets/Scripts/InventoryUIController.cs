using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject bookUI;
    public Button inventoryIcon;
    public Transform gridParent;
    public GameObject slotPrefab;
    public Image selectionBox;
    public Image descriptionBox;
    public TMP_Text descriptionText;
    public TMP_Text controlText;

    [Header("Collectable Items in Order")]
    public List<CollectableItem> allItemsInOrder; // Set in Inspector

    private List<InventorySlot> slots = new();
    private int selectedIndex = 0;

    void Start()
    {
        if (bookUI) bookUI.SetActive(false);
        if (inventoryIcon) inventoryIcon.onClick.AddListener(ToggleInventory);

        selectionBox.enabled = false;
        RefreshInventoryUI();

        InventoryManager.Instance.onInventoryChangedCallback += RefreshInventoryUI;
        if (descriptionBox != null && descriptionBox.rectTransform != null)
        {
            descriptionBox.rectTransform.anchoredPosition3D = new Vector3(231.698f, -78.5437f, 0f);
        }

        // Set grid container position
        if (gridParent != null && gridParent.GetComponent<RectTransform>() != null)
        {
            gridParent.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-332.302f, 207.4563f, 0f);
        }

        // Set selection box initial position (can be overridden in MoveSelection)
        if (selectionBox != null && selectionBox.rectTransform != null)
        {
            selectionBox.rectTransform.anchoredPosition3D = new Vector3(-339.302f, 215f, 0f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) ToggleInventory();
        if (!bookUI.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveSelection(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveSelection(-1);
        if (Input.GetKeyDown(KeyCode.UpArrow)) MoveSelection(-3);
        if (Input.GetKeyDown(KeyCode.DownArrow)) MoveSelection(3);

        if (Input.GetKeyDown(KeyCode.H)) HoldSelectedItem();
        if (Input.GetKeyDown(KeyCode.U)) UseSelectedItem();
    }

    void ToggleInventory()
    {
        bool isOpen = bookUI.activeSelf;
        bookUI.SetActive(!isOpen);

        var player = GameObject.FindWithTag("Player")?.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.canMove = !bookUI.activeSelf;

            if (!player.canMove)
            {
                // force stop movement when inventory opens
                player.ResetMovement();
            }
        }

        if (!isOpen)
        {
            selectedIndex = 0;
            UpdateSlotIcons();
            StartCoroutine(DelayedMoveSelection());
        }
        else
        {
            selectionBox.enabled = false;
        }
    }

    void RefreshInventoryUI()
    {
        if (gridParent == null || slotPrefab == null || allItemsInOrder.Count == 0)
        {
            Debug.LogWarning("InventoryUIController: Missing references or empty item list.");
            return;
        }

        foreach (Transform child in gridParent) Destroy(child.gameObject);
        slots.Clear();

        for (int i = 0; i < allItemsInOrder.Count; i++)
        {
            bool collected = InventoryManager.Instance.HasItem(allItemsInOrder[i].itemName);
            GameObject slotObj = Instantiate(slotPrefab, gridParent);

            Image iconImage = slotObj.transform.Find("Icon").GetComponent<Image>();
            iconImage.rectTransform.localScale = new Vector3(0.0016f, 0.0009f, 1f);

            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.SetSlot(allItemsInOrder[i], collected);
            slots.Add(slot);
        }

        StartCoroutine(DelayedMoveSelection());
        controlText.text = "-> Overworld:\n-Move: Arrow Keys or WASD\n-Interact/Leave Room: Press E\n-Open Inventory Book: Press Z\n-> Inventory Book:\n-Navigate: Arrows\n-Hold Item: H\n-Use Item: U\n-Close Inventory Book: Z";
    }

    void MoveSelection(int delta)
    {
        if (slots.Count == 0) return;

        selectedIndex = Mathf.Clamp(selectedIndex + delta, 0, slots.Count - 1);
        RectTransform selectedTransform = slots[selectedIndex].GetComponent<RectTransform>();

        selectionBox.rectTransform.position = selectedTransform.position;
        selectionBox.enabled = true;

        var item = slots[selectedIndex].pieceData;
        bool hasItem = InventoryManager.Instance.HasItem(item.itemName);

        descriptionText.text = hasItem ? item.description : "????";

        if (hasItem)
        {
            if (InventoryManager.Instance.itemInHand == item)
            {
                descriptionText.text += "\n<size=130%><color=red>[IN HAND]</color></size>";
            }
        }
    }

    private IEnumerator DelayedMoveSelection()
    {
        yield return null;
        MoveSelection(0);
    }

    void UpdateSlotIcons()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            bool hasItem = InventoryManager.Instance.HasItem(slots[i].pieceData.itemName);
            slots[i].SetSlot(slots[i].pieceData, hasItem);
        }
    }

    public bool IsInventoryOpen()
    {
        return bookUI != null && bookUI.activeSelf;
    }

    void HoldSelectedItem()
    {
        if (slots.Count == 0) return;
        var item = slots[selectedIndex].pieceData;
        if (InventoryManager.Instance.HasItem(item.itemName))
        {
            InventoryManager.Instance.HoldItem(item);
            RefreshInventoryUI();
        }
    }

    void UseSelectedItem()
    {
        GameObject target = GameObject.Find("Wally");
        if (target != null)
        {
            InventoryManager.Instance.UseItemInFront(target);
        }
    }
}

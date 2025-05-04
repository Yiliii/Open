using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public CollectableItem pieceData;
    public Sprite questionMarkSprite;

    public void SetSlot(CollectableItem data, bool hasCollected)
    {
        pieceData = data;

        if (hasCollected && data.icon != null)
        {
            icon.sprite = data.icon;
        }
        else if (questionMarkSprite != null)
        {
            icon.sprite = questionMarkSprite;
        }
        else
        {
            Debug.LogWarning($"Missing icon or questionMarkSprite for {data.itemName}");
            icon.enabled = false;
            return;
        }

        icon.color = Color.white;
        icon.enabled = true;
        icon.preserveAspect = true;
    }
}
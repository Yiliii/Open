using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "OpenGame/Item")]
public class CollectableItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
}

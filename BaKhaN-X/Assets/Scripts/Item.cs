using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject // not need attach script at a object
{
    public string itemName; // name of item
    public Sprite itemImage; // image of item
    public ItemType itemType;
    public GameObject itemPrefab; // prefab of item

    public string weaponType; // type of weapon

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        Etc
    }
}
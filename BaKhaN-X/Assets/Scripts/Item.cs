using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; // name of item
    public Sprite itemImage; // image of item

    public GameObject itemPrefab; // prefab of item

    public string weaponType; // type of weapon

    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        Etc
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

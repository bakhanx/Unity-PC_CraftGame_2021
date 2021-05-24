using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotToolTip : MonoBehaviour
{
    [SerializeField] private GameObject go_Base;
    [SerializeField] private Text txt_ItemName;
    [SerializeField] private Text txt_ItemDesc;
    [SerializeField] private Text txt_ItemUseMethod;

    public void ShowToolTip(Item _item)
    {
        go_Base.SetActive(true);

        txt_ItemName.text = _item.itemName;
        txt_ItemDesc.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
            txt_ItemUseMethod.text = "[RMB] : Equipment";
        else if (_item.itemType == Item.ItemType.Used)
            txt_ItemUseMethod.text = "[RMB] : Eat";
        else
            txt_ItemUseMethod.text = "";
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}

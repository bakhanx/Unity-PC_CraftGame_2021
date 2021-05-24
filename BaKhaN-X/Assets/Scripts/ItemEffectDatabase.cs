using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // item name : key
    [Tooltip("HP,SP,DP,HUNGRY,THIRSTY,SATISFY")]
    public string[] part; // part
    public int[] num; // plus / minus
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField] private ItemEffect[] itemEffects;

    //component
    [SerializeField] private StatusController thePlayerStatus;
    [SerializeField] private WeaponManager theWeaponManager;
    [SerializeField] private SlotToolTip theSlotToolTip;

    private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void ShowToolTip(Item _item)
    {
        theSlotToolTip.ShowToolTip(_item);
    }

    public void HideToolTip()
    {
        theSlotToolTip.HideToolTip();
    }

    public void UseItem(Item _item)
    {
        //Equip item
        if (_item.itemType == Item.ItemType.Equipment)
        {
            //ex) "GUN", "Submachinegun"
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
        }
        //Use item
        else if (_item.itemType == Item.ItemType.Used)
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHP(itemEffects[i].num[j]);
                                break;
                            case SP:
                                // thePlayerStatus.IncreaseSP(itemEffects[i].num[j]);
                                break;
                            case DP:
                                thePlayerStatus.IncreaseDP(itemEffects[i].num[j]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.IncreaseThirsty(itemEffects[i].num[j]);
                                break;
                            case HUNGRY:
                                thePlayerStatus.IncreaseHungry(itemEffects[i].num[j]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("Error : Not status part.(HP,SP,DP,HUNGRY,THIRSTY,SATISFY)");
                                break;
                        }
                        Debug.Log("Used " + _item.itemName);
                    }
                    return;
                }

            }
        }
        Debug.Log("Not match the itemName on ItemEffectDatabase");
    }
}

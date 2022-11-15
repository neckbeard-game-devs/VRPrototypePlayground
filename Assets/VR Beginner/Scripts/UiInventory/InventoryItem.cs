using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem : ScriptableObject
{
    [Header("InventoryItem Info")]
    public Sprite icon = null;
    public string item_Name;
    public string item_Id;

    [TextArea]
    public string item_Desc;

    public bool is_Sellable;
    public bool is_Broken;
    public bool is_Legendary;

    public int item_Weight;
    public int item_MaxDurability;
    public int item_CurrentDurability;

    public int sellPrice = 1;
    public int maxStack = 1;

    public void ReduceDurability(int amount)
    {
        if (!is_Broken)
        {
            item_CurrentDurability -= amount;
            if (item_CurrentDurability <= 0)
            {
                is_Broken = true;
                item_CurrentDurability = 0;
            }
        }
    }
    public void RepairItem()
    {
        is_Broken = false;
        item_CurrentDurability = item_MaxDurability;
    }
    public abstract void Use();
    public int Buy()
    {
        return sellPrice;
    }
    public void Sell()
    {
        if (is_Sellable)
        {

        }
    }
    public int SellPrice { get { return sellPrice; } }
    public int MaxStack { get { return maxStack; } }
}

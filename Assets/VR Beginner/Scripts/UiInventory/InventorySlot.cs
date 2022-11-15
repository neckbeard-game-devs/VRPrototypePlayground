using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InventorySlot : MonoBehaviour
{
    public InventoryItem item;
    public SpriteRenderer icon;
    public int quantity;
    public string itemName;
    public bool testing;

    public void SetValues(InventoryItem _item, int _value)
    {
        if (item != null)
        {
            item = _item;
            quantity = _value;
            icon.sprite = item.icon;
            itemName = item.item_Name;
            icon.gameObject.name = itemName + " " + quantity;

        }
    }
    private void Update()
    {
        if (testing)
        {
            testing = false;
            SetValues(item, 5);
        }
    }
}

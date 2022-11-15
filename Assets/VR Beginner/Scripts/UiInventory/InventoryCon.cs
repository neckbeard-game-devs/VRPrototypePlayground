using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class InventoryCon : MonoBehaviour
{
    public Inventory inventory;
    public InventorySlot iSlot;
    public bool testing;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (testing)
        {
            testing = false;
            inventory.swords = iSlot.quantity;
        }
    }
}

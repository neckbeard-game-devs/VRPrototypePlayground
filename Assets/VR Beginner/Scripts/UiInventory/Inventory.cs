using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{

    public int swords;

    [ContextMenu("Test Add")]
    public void TestAdd()
    {

    }
}

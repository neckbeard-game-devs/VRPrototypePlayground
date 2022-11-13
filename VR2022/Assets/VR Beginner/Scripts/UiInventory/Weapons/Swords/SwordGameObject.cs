using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGameObject : MonoBehaviour
{
    public SwordItem sword;

    public string sword_Name;
    public int sword_Damage;

    private void Start()
    {
        sword_Damage = sword.item_Damage;
        sword_Name = sword.item_Name;

        //sword.Use();
    }
}

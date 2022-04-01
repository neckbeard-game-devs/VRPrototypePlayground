using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LadderCon : MonoBehaviour
{
    private AnimParent animScript;
    public Animator ladderAnim;
    public Collider ladderCol;
    public Transform ladderParentGo;

    public bool setup;
    void Update()
    {
        if (setup)
        {
            setup = false;
            ladderAnim = GetComponent<Animator>();
            ladderCol = GetComponent<Collider>();
            ladderParentGo = GetComponentInChildren<AnimParent>().gameObject.transform;
        }
    }
}

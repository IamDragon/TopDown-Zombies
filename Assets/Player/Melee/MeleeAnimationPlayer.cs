using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationPlayer : AnimationPlayer
{
    [SerializeField] protected string animLeftSideName;

    protected override void PlayAnimation()
    {
        if(HelperFunctions.GetDirToMouse(transform.position).x > 0) //play right side anim
        {
            StartAnimation(animName);
        }
        else //play left side anim
            StartAnimation(animLeftSideName);
            Debug.Log(HelperFunctions.GetDirToMouse(transform.position).x);
    }
}

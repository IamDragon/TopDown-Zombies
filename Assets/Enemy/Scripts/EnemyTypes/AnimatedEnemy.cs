using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedEnemy : Enemy
{
    Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void Update()
    {
        if (target == null)
        {
            //stand still don't do jack shit
            movement.SetRBVel0();
            StartRunAnimation();
            return;
        }

        if (attackHandler.TargetInAttackRange(target))
        {
            movement.SetRBVel0();
            attackHandler.PerformAttack(HelperFunctions.GetDirToTarget(transform, target));
            //Attack
        }
        else// if(!EntranceInRange())
        {
            MoveToNextNode();
            StartIdleAnimation();
        }

    }

    protected void StartRunAnimation()
    {
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdling", false);
    }

    protected void StartIdleAnimation()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdling", true);
    }
}

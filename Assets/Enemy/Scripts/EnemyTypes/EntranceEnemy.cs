using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceEnemy : AnimatedEnemy
{
    [Header("Entrance")]
    [SerializeField] protected float distanceToEntrance;
    [SerializeField] protected LayerMask entranceMask;
    public BoardedEntrance closestEntrance;

    protected override void Update()
    {
        if (target == null)
        {
            //stand still don't do jack shit
            movement.SetRBVel0();
            StartIdleAnimation();

            return;
        }

        //attack anims not working so we be skipping attacking and just deal dmg on collision
        //if (attackHandler.TargetInAttackRange(target))
        //{
        //    movement.SetRBVel0();
        //    attackHandler.PerformAttack(HelperFunctions.GetDirToTarget(transform, target));
        //    //Attack
        //}
        if (EntranceInRange() && closestEntrance != null && !closestEntrance.IsBroken) // could cause problems if enemy is right outside of entrance that isnt broken
        {
            movement.SetRBVel0();
            if (closestEntrance.CanBreak)
                closestEntrance.BreakBoard();
        }
        else// if(!EntranceInRange())
        {
            StartRunAnimation();
            MoveToNextNode();
        }

    }

    protected bool EntranceInRange()
    {
        Collider2D entrance = Physics2D.OverlapCircle(transform.position, distanceToEntrance, entranceMask);
        if (entrance)
            closestEntrance = entrance.transform.parent.GetComponent<BoardedEntrance>();
        else
            closestEntrance = null;
        return entrance;
    }


}

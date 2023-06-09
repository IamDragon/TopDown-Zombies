// Ignore Spelling: Gizmos

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttackHandler : AttackHandler
{
    [SerializeField] private float distanceToAttack; // how close target needs to be to attack target - this is separate from attack range

    public bool TargetInAttackRange(Transform target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= distanceToAttack)
            return true;
        return false;
        //return Physics2D.OverlapCircle(transform.position, distanceToAttack, damageInfo.maskToDamage);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if(DrawGizmo)
            Gizmos.DrawWireSphere(transform.position,distanceToAttack);
    }


    //this will be the only attack since attack animations aren't really working
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(damageInfo.tagToDamage))
            collision.transform.GetComponent<HitHandler>().GetHit(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : AttackHandler
{
    protected override void DealDamage(Transform objectTodamage)
    {
        objectTodamage.GetComponent<EnemyHitHandler>().GetHitMelee(damage, transform.position);
    }
}

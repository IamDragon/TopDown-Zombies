using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public static Action OnTakeDamage;
    public static Action OnNormalDeath;

    public static Action OnHit;
    public static Action OnSpecialDeath;
    public static Action OnHeadshotDeath;
    public static Action OnKnifeDeath;


    //bullet checks hit body or hit head
    //trigger OnHit reps. OnHeadshot
    //keep variable somewhere bool lastHitHeadshot
    //on death
    //  if(lastHitHeadshot) OnDeath(100)
    //  else OnDeath(70)
}

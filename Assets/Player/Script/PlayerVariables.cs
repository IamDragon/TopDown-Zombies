using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    [Header("Movement")]
    public float speedMultiplier = 1;
    public float maxWalkSpeed;
    public float maxRunSpeed;
    public float downedSpeed;

    [Header("Enemy Interaction")]
    public DamageInfo damageInfo;

    [Header("Grenade")]
    public float grenadeSpeed;
    public float grenadeRange;

    [Header("Revive")]
    public float reviveTime;

    [Header("State")]
    public bool isDowned;
}

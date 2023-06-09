using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName ="DamageInfo")]
public class DamageInfo : ScriptableObject
{
    public LayerMask maskToDamage;
    public string tagToDamage;
    public string headTag;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "PointInfo")]
public class PointInfo : ScriptableObject
{
    public int hitPoints;
    public int killPoints;
    public int headshotPoints;
}

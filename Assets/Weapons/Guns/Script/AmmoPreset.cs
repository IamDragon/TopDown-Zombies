using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "AmmoPreset")]
public class AmmoPreset : ScriptableObject
{
    public int maxMagAmmo;
    public int startingAmmo;
    public int maxAmmoCapacity;
}

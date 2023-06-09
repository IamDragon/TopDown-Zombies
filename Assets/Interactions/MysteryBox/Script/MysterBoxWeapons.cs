using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "MysterBoxWeapons")]
public class MysterBoxWeapons : ScriptableObject
{
    public List<MysteryBoxWeapon> guns;
    public List<float> percentages;

    private MysteryBoxWeapon lastGun;
    private MysteryBoxWeapon newestGun;

    public MysteryBoxWeapon GetRandomWeapon()
    {
        int index = Random.Range(0, guns.Count);
        Debug.Log("Gun from box: " + guns[index]);

        lastGun = newestGun; //set lastgun to old newestGun
        newestGun = guns[index];  //set newEstgun to the fouynd gun
        return guns[index];
    }

    /// <summary>
    /// Return a gun which is different from the last
    /// </summary>
    /// <returns></returns>
    public MysteryBoxWeapon GetNewRandomGun()
    {
        if (lastGun == null && newestGun == null)
            return GetRandomWeapon();

        MysteryBoxWeapon gun = GetRandomWeapon();
        while (gun == lastGun)
        {
            gun = GetRandomWeapon();
            if (gun != lastGun)
            {
                lastGun = gun;
                break;
            }
        }
        return gun;
    }
}

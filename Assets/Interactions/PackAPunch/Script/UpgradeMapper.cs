using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenuAttribute(menuName = "UpgradeMapper")]
public class UpgradeMapper : ScriptableObject
{
    public Gun[] baseGuns;
    public Gun[] upgradedGuns;

    public Gun GetUpgradedWeapon(Gun baseGun)
    {
        Debug.Log(baseGuns.Length);
        Debug.Log(upgradedGuns.Length);
        int upgradedGunIndex = 0;
        for(int i = 0; i < baseGuns.Length; i++)
        {
            if (baseGun.GunName == baseGuns[i].GunName)
            {
                upgradedGunIndex = i;
                break;
            }
        }


        if(upgradedGunIndex >= upgradedGuns.Length)
        {
            Debug.LogWarning("weapon no existo sorry friendo fix ur problemo");
            return null;
        }

        Debug.Log(baseGun);
        Debug.Log(upgradedGuns[upgradedGunIndex]);

        return upgradedGuns[upgradedGunIndex];
    }
}

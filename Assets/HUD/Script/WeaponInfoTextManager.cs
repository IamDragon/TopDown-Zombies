using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponInfoTextManager : MonoBehaviour
{
    [Header("Holders")]
    [SerializeField] private TextMeshProUGUI magText;
    [SerializeField] private TextMeshProUGUI stockpileText;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Events")]
    [SerializeField] private GunEventSO onSwitchGun;
    [SerializeField] private IntIntEventSO updateAmmoInfoEvent;

    private void OnEnable()
    {

        onSwitchGun.Action += UpdateGuninfo;
        updateAmmoInfoEvent.Action += UpdateAmmoInfo;
    }

    private void OnDisable()
    {
        onSwitchGun.Action -= UpdateGuninfo;
        updateAmmoInfoEvent.Action -= UpdateAmmoInfo;
    }

    private void UpdateGuninfo(Gun gun)
    {
        if (gun == null)
        {
            UpdateAmmoInfo(0, 0);
            nameText.text = "";
        }
        else
        {
            UpdateAmmoInfo(gun.CurrentMagAmmo, gun.StockpileAmmo);
            nameText.text = gun.GunName;
        }

    }

    private void UpdateAmmoInfo(int mag, int stockpile)
    {
        stockpileText.text = stockpile.ToString();
        magText.text = mag.ToString();
    }


}

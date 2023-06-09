using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Perk : ScriptableObject
{
    [SerializeField] private string perkName;
    [SerializeField] private Sprite icon;
    public string PerkName { get { return perkName; } }

    [Header("Events")]
    [SerializeField] private SpriteEventSO onActivatePerk;

    public virtual void ActivatePerk()
    {
        Debug.Log(this + "perk activated");
        onActivatePerk.Invoke(icon);
    }

    public virtual void DeactivatePerk()
    {
        Debug.Log(this + "perk deactivated");
    }

    public virtual void GetComponentHandler(GameObject player)
    {

    }
}

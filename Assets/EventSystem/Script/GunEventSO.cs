using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/GunEvent")]
public class GunEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<Gun> Action;

    public void Invoke(Gun gun) => Action?.Invoke(gun);
}

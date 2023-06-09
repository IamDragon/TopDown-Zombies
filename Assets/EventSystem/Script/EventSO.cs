using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Event")]
public class EventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action Action;

    public void Invoke() => Action?.Invoke();
}

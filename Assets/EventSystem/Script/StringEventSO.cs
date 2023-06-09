using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/StringEventSO")]
public class StringEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<string> Action;

    public void Invoke(string s) => Action?.Invoke(s);
}

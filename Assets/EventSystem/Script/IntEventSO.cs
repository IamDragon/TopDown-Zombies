using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/IntEvent")]
public class IntEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<int> Action;

    public void Invoke(int i) => Action?.Invoke(i);
}

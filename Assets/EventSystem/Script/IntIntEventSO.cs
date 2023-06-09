using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/IntIntEvent")]
public class IntIntEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<int, int> Action;

    public void Invoke(int i, int j) => Action?.Invoke(i, j);
}

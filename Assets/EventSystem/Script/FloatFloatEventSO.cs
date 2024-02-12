using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/FloatFloatEventSO")]
public class FloatFloatEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<float, float> Action;

    public void Invoke(float f1, float f2) => Action?.Invoke(f1, f2);
}

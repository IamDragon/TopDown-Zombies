using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/TransformEventSO")]
public class TransformEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<Transform> Action;

    public void Invoke(Transform transform) => Action?.Invoke(transform);
}

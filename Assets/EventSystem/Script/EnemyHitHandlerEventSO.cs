using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/EnemyHitHandlerEvent")]
public class EnemyHitHandlerEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<EnemyHitHandler> Action;

    public void Invoke(EnemyHitHandler handler) => Action?.Invoke(handler);
}

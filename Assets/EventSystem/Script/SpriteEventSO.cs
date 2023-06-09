using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Event/SpriteEventSO")]
public class SpriteEventSO : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;

    public event Action<Sprite> Action;

    public void Invoke(Sprite sprite) => Action?.Invoke(sprite);
}

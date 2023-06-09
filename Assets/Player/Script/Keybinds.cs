using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    [SerializeField] private static KeyCode interactionButton;

    public static KeyCode InteractionButton { get { return interactionButton; } }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{

    //[Header("Action")]
    [SerializeField] private static KeyCode interactionButton;
    //[SerializeField] private static KeyCode fireButton;
    //[SerializeField] private static KeyCode grenadeButton;

    //[Header("Menu")]
    //[SerializeField] private static KeyCode menuButton;

    //[Header("Movement")]
    //[SerializeField] private static KeyCode moveLeft;
    //[SerializeField] private static KeyCode moveLeft;
    //[SerializeField] private static KeyCode moveUp;
    //[SerializeField] private static KeyCode moveUp;
    //[SerializeField] private static KeyCode sprintButton;

    public static KeyCode InteractionButton { get { return interactionButton; } }
    //public static KeyCode FireButton { get { return fireButton; } }
    //public static KeyCode GrenadeButton { get { return grenadeButton; } }

    //public static KeyCode MenuButton { get { return menuButton; } }

    //public static KeyCode SprintButton { get { return sprintButton; } }
}

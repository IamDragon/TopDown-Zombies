// Ignore Spelling: Keybinds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    public static Keybinds Instance { get; private set; }


    [Header("Action")]
    //public KeyCode interactionButton;
    [SerializeField] private KeyCode interactionButton = KeyCode.E;
    [SerializeField] private KeyCode fireButton;
    [SerializeField] private KeyCode reloadButton;
    [SerializeField] private KeyCode grenadeButton;

    [Header("Action")]
    [SerializeField] private KeyCode switchWeapon1;
    [SerializeField] private KeyCode switchWeapon2;
    [SerializeField] private KeyCode switchWeapon3;


    [Header("Menu & HUD")]
    [SerializeField] private KeyCode menuButton;
    [SerializeField] private KeyCode scoreBoardButton;

    [Header("Movement")]
    [SerializeField] private KeyCode moveLeftButton;
    [SerializeField] private KeyCode moveRightButton;
    [SerializeField] private KeyCode moveUpButton;
    [SerializeField] private KeyCode moveDownButton;
    [SerializeField] private KeyCode sprintButton;


    //action
    public bool InteractionButtonDown { get { return Input.GetKeyDown(interactionButton); } }
    //public bool InteractionButtonBeingPressed { get { return Input.GetKeyDown(interactionButton); } }
    public KeyCode InteractionButton { get { return interactionButton; } }

    public bool FireButtonDown { get { return Input.GetKeyDown(fireButton); } }
    public KeyCode FireButton { get { return fireButton; } }

    public bool ReloadButtonDown { get { return Input.GetKeyDown(reloadButton); } }
    public KeyCode ReloadButton { get { return reloadButton; } }

    public bool GrenadeButtonDown { get { return Input.GetKeyDown(grenadeButton); } }
    public KeyCode GrenadeButton { get { return grenadeButton; } }

    //weapon switching
    public bool SwitchWeapon1Down { get { return Input.GetKeyDown(switchWeapon1); } }
    public KeyCode SwitchWeapon1 { get { return switchWeapon1; } }
    public bool SwitchWeapon2Down { get { return Input.GetKeyDown(switchWeapon2); } }
    public KeyCode SwitchWeapon2 { get { return switchWeapon2; } }
    public bool SwitchWeapon3Down { get { return Input.GetKeyDown(switchWeapon3); } }
    public KeyCode SwitchWeapon3 { get { return switchWeapon3; } }

    //menu
    public bool MenuButtonDown { get { return Input.GetKeyDown(menuButton); } }
    public KeyCode MenuButton { get { return menuButton; } }
    public bool ScoreBoardButtonDown { get { return Input.GetKeyDown(scoreBoardButton); } }
    public KeyCode ScoreBoardButton { get { return scoreBoardButton; } }

    //movement
    public bool MoveLeftButtonDown { get { return Input.GetKeyDown(moveLeftButton); } }
    public KeyCode MoveLeftButton { get { return moveLeftButton; } }
    public bool MoveRightButtonDown { get { return Input.GetKeyDown(moveRightButton); } }
    public KeyCode MoveRightButton { get { return moveRightButton; } }
    public bool MoveUpButtonDown { get { return Input.GetKeyDown(moveUpButton); } }
    public KeyCode MoveUpButton { get { return moveUpButton; } }
    public bool MoveDownButtonDown { get { return Input.GetKeyDown(moveDownButton); } }
    public KeyCode MoveDownButton { get { return moveDownButton; } }
    public bool SprintButtonDown { get { return Input.GetKeyDown(fireButton); } }
    public KeyCode SprintButton { get { return fireButton; } }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this.gameObject);
    }
}

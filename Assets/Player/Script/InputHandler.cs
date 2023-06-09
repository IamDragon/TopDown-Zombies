using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected EventSO onUse;
    [SerializeField] protected EventSO onPauseToggled;
    //[SerializeField] protected EventSO onMelee;

    PlayerVariables vars;
    Movement move;
    WeaponHandler weaponHandler;
    GrenadeHandler grenadeHandler;
    AttackHandler attackHandler;
    PlayerStats stats;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        vars = GetComponent<PlayerVariables>();
        move = GetComponent<Movement>();
        weaponHandler = GetComponentInChildren<WeaponHandler>();
        grenadeHandler = GetComponent<GrenadeHandler>();
        attackHandler = GetComponentInChildren<AttackHandler>();
        stats = GetComponent<PlayerStats>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }



    void Update()
    {
        PauseGame();
        if (PauseManager.GamePaused) return;
        MovementInput();
        FireInput();
        ReloadInput();
        //MeleeInput(); //melee anims not working wont be using this
        ScoreBoardInput();
        FlipSprite();

        if (vars.isDowned) // if player is down cant do shit bellow - should probs be in all the individual functions
            return;

        WeaponSwitchInput();
        GrenadeInput();
        UseInput();
    }

    private void MovementInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float runButtonDown = Input.GetAxisRaw("Run");

        if (vars.isDowned)
            DownedMovement(input);
        else
            StandingMovement(input, runButtonDown);



    }

    private void DownedMovement(Vector2 input)
    {
        if (input.magnitude != 0)
            move.SetVelocity(input, vars.downedSpeed);
        else
            move.SetRBVel0();
    }

    private void StandingMovement(Vector2 input, float runButtonDown)
    {
        if (input.magnitude != 0 && runButtonDown == 1)
        {
            move.SetVelocity(input, vars.maxRunSpeed * vars.speedMultiplier);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdling", false);
        }
        else if (input.magnitude != 0 && runButtonDown == 0)
        {
            move.SetVelocity(input, vars.maxWalkSpeed * vars.speedMultiplier);
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdling", false);
        }
        else
        {
            move.SetRBVel0();
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdling", true);
        }
    }

    private void FireInput()
    {
        float fire = Input.GetAxisRaw("Fire1");
        if (fire > 0)
        {
            weaponHandler.PullGunTrigger();
        }
        else
            weaponHandler.ReleaseGunTrigger();
    }

    private void ReloadInput()
    {
        float reload = Input.GetAxisRaw("Reload");
        if (reload > 0)
            weaponHandler.ReloadCurrentGun();
    }

    private void WeaponSwitchInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponHandler.SwitchToGun(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
            weaponHandler.SwitchToGun(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && weaponHandler.CanHoldExtraGun)
            weaponHandler.SwitchToGun(2);
    }

    private void GrenadeInput()
    {
        if (Input.GetKeyDown(KeyCode.G))
            grenadeHandler.ThrowGrenade(HelperFunctions.GetDirToMouse(transform.position), vars.grenadeSpeed);
    }

    private void UseInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            onUse.Invoke();
    }

    private void MeleeInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            attackHandler.PerformAttack(HelperFunctions.GetDirToMouse(transform.position));
            //onMelee.Invoke();
        }
    }

    private void ScoreBoardInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            HUDScoreboard.Instance.ToggleScoreboard(stats);
    }

    private void FlipSprite()
    {
        Vector2 dir = HelperFunctions.GetDirToMouse(transform.position);
        if(dir.x < 0)
            spriteRenderer.flipX= true;
        else
            spriteRenderer.flipX = false;
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onPauseToggled.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.right * 1);

    }
}

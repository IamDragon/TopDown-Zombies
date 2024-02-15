using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : WeaponBuyDelayed
{
    [SerializeField] private MysterBoxWeapons mysteryBoxWeapons;
    [SerializeField] private AudioClip boxJingle;
    private MysteryBoxManager mysteryBoxManager;
    private MysteryBoxWeapon boxGun;
    private Animator animator;
    private ShowRandomWeaponAnimation weaponAnimation;

    public MysterBoxWeapons MysterBoxWeapons { get { return mysteryBoxWeapons; } }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        weaponAnimation = GetComponentInChildren<ShowRandomWeaponAnimation>();
        weaponAnimation.Init(this);
    }

    public void ActivateBox()
    {
        gameObject.SetActive(true);//need to play animation for spawning box
    }

    public void DeactivateBox()
    {
        gameObject.SetActive(false);//animation needs to be played when box is removed
    }

    public void Init(MysteryBoxManager manager, int mysteryBoxcost)
    {
        mysteryBoxManager = manager;
        cost = mysteryBoxcost;
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
    }

    protected override void SetInteractionText()
    {
        if (finished)
            interaction.SetInteractionText(" pick up " + boxGun.name);
        else
            interaction.SetInteractionText(" purchase mystery box " + costText);
    }

    protected override void InitialInteraction()
    {
        base.InitialInteraction();
        //start animation
        Debug.Log("initial interaction");

        //animation - animation starts random weapon shuffle once box is opened
        //Invoke(nameof(weaponAnimation.StartAnimation), )
        weaponAnimation.StartAnimation();
        audioSource.PlayOneShot(boxJingle);
        animator.Play("Chest_empty_open");
    }

    protected override void FinishProcess()
    {
        weaponAnimation.StopAnimation();
        if (mysteryBoxManager.BoxPurchase())
        {
            CancelInvoke();
            pointManager.AddPoints(cost);
            //show teddy bear sprite
        }
        else
        {
            boxGun = mysteryBoxWeapons.GetRandomWeapon();
            weaponAnimation.SetFinalSprite(boxGun.SpriteRenderer.sprite);
            Debug.Log("found gun");
        }
        base.FinishProcess();
    }

    protected override void PickUp()
    {
        base.PickUp();
        if (boxGun.TryGetComponent<Projectile>(out Projectile grenade))
            Player.Instance.GrenadeHandler.RecieveNewGrenadeType(grenade);
        else
            weaponHandler.ReceiveNewGun(boxGun.GetComponent<Gun>());
        Debug.Log("picked up gun");

        //animation hides weapon once it starts closing
        weaponAnimation.Hide();
        animator.Play("Chest_empty_close");
    }

    protected override void RemoveWeapon()
    {
        base.RemoveWeapon();
        boxGun = null;
        Debug.Log("removing gun");
        animator.Play("Chest_empty_close");
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MysteryBox : WeaponBuyDelayed
{
    [SerializeField] private MysterBoxWeapons mysteryBoxWeapons;
    [SerializeField] private Sprite boxMoveSprite;
    private MysteryBoxManager mysteryBoxManager;
    private MysteryBoxWeapon boxGun;
    private Animator animator;
    private bool moving;
    private ShowRandomWeaponAnimation weaponAnimation;
    [Header("Audio")]
    [SerializeField] private AudioClip boxJingle;
    [SerializeField] private AudioClip boxMoveAudio;

    public MysterBoxWeapons MysterBoxWeapons { get { return mysteryBoxWeapons; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        weaponAnimation = GetComponentInChildren<ShowRandomWeaponAnimation>();
    }

    protected override void Start()
    {
        base.Start();
        weaponAnimation.Init(this);
    }

    public void ActivateBox()
    {
        gameObject.SetActive(true);//need to play animation for spawning box
    }

    public void DeactivateBox()
    {
        gameObject.SetActive(false);//animation needs to be played when box is removed
        moving = false;
        working = false;
        weaponAnimation.Hide();
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
        weaponAnimation.StartAnimation();
        audioSource.PlayOneShot(boxJingle);
        animator.Play("Chest_empty_open");
    }

    protected override void DoThing()
    {
        if (moving)
            return;
        base.DoThing();
    }

    protected override void BuyThing()
    {
        if (moving)
            return;
        base.BuyThing();
    }

    protected override void FinishProcess()
    {
        weaponAnimation.StopAnimation();
        if (mysteryBoxManager.BoxPurchase())
        {
            Player.Instance.PointManager.AddPoints(cost);
            CancelInvoke();
            weaponAnimation.SetFinalSprite(boxMoveSprite);
            audioSource.PlayOneShot(boxMoveAudio);
            moving = true;
        }
        else
        {
            boxGun = mysteryBoxWeapons.GetRandomWeapon();
            weaponAnimation.SetFinalSprite(boxGun.SpriteRenderer.sprite);
            Debug.Log("found gun");
            base.FinishProcess();
        }
    }

    protected override void PickUp()
    {
        base.PickUp();
        if (boxGun.TryGetComponent<Projectile>(out Projectile grenade))
            Player.Instance.GrenadeHandler.RecieveNewGrenadeType(grenade);
        else
            Player.Instance.WeaponHandler.ReceiveNewGun(boxGun.GetComponent<Gun>());
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
        weaponAnimation.Hide();
    }
}

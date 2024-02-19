using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class ShowRandomWeaponAnimation : MonoBehaviour
{
    [SerializeField] private float animaionTime;
    [SerializeField] private float initialDuration = 1;
    [SerializeField] private float durationIncrease = 0.1f;

    public int spriteChangesPerformed; //how many times sprite has been changed
    public float timer; // animationtimer
    private bool running;

    private MysterBoxWeapons boxWeapons;
    private MysteryBox mysteryBox;
    private SpriteRenderer spriterenderer;

    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        spriterenderer.enabled = false;
        //durationDecrease = (initialDuration - 0.01f) / spriteChanges;
    }

    public void Init(MysteryBox mysteryBox)
    {
        this.mysteryBox = mysteryBox;
        boxWeapons = mysteryBox.MysterBoxWeapons;
    }

    //boxopen triggers this event to start showing random weapon
    public void StartAnimation()
    {
        //ChangeSprite(); //intially set sprite
        //Invoke(nameof(ChangeSprite), CalculateTimeTillNextChange());
        //Debug.Log("weapon shuffle anim started");
        timer = 0f;
        spriteChangesPerformed = 0;
        spriterenderer.enabled = true;
        running = true;
        StartCoroutine(ChangeSprite());
    }

    public void SetFinalSprite(Sprite sprite)
    {
        spriterenderer.sprite = sprite;
    }

    private IEnumerator ChangeSprite()
    {


        //set image then wait -> repeat until finished
        while (running)
        {
            spriterenderer.sprite = GetRandomWeaponImage();

            float duration = (spriteChangesPerformed * durationIncrease) - initialDuration;

            //waitTime = CalculateTimeTillNextChange();


            yield return new WaitForSeconds(duration);
            //yield return null;
            //spriteChangesPerformed = (spriteChangesPerformed + 1) % spriteChanges;
            spriteChangesPerformed++;
            timer += duration;
        }

    }

    public void StopAnimation()
    {
        running = false;
        //StopCoroutine(ChangeSprite());
    }

    public Sprite GetRandomWeaponImage()
    {
        return boxWeapons.GetNewRandomGun().SpriteRenderer.sprite;
    }

    public void Hide()
    {
        //hide sprite but don't disable object
        running = false;
        spriterenderer.enabled = false;
        timer = 0f;
        spriteChangesPerformed = 0;
    }

    /*
     * start show random image of weapon from mysterBoxwepaons
     * after time show new weapon 
     * repeat for an amount of time - slow down after every switch
     * show final weapon
     */
}

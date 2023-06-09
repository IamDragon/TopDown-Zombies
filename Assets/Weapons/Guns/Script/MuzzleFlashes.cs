using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashes : MonoBehaviour
{
    [SerializeField] private List<Sprite> muzzleFlashes;
    [SerializeField] private float angle;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled= false;
    }
    public void ShowMuzzleFlash(float time)
    {
        //gameObject.SetActive(true);
        spriteRenderer.enabled= true;
        SetRandomSprite();
        Invoke(nameof(HideMuzzleFlash), time);
        Debug.Log("show flash");
    }

    private void HideMuzzleFlash()
    {
        spriteRenderer.enabled= false;
        //gameObject.SetActive(false);
    }

    private void SetRandomSprite()
    {
        spriteRenderer.sprite = muzzleFlashes[Random.Range(0, muzzleFlashes.Count-1)];
    }

}

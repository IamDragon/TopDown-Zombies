using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected EventSO playAnimEvent;

    [Header("Animation")]
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected float animTime;
    [SerializeField] protected string animName;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        playAnimEvent.Action += PlayAnimation;
    }

    protected virtual void OnDisable()
    {
        playAnimEvent.Action -= PlayAnimation;
    }

    protected virtual void PlayAnimation()
    {
        StartAnimation(animName);
    }

    protected virtual void StartAnimation(string animationName)
    {
        Debug.Log("Playing anim");
        sprite.enabled = true;
        animator.Play(animName);
        Invoke(nameof(HideSprite), animTime);
    }

    protected virtual void HideSprite()
    {
        sprite.enabled = false;
    }
}

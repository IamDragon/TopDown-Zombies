using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedVFX : MonoBehaviour
{
    public AnimatedVFXManager.VFXType type;
    [Tooltip("the amount fo animations the animator contains, -1 if only has one")]
    [SerializeField] private int animationCount;
    [Tooltip("the base name for all animations ex. attack(1-n)")]
    [SerializeField] private string animationName;
    [SerializeField] private Animator animator;
    private bool active;
    public bool Active { get { return active; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        active = true;
        gameObject.SetActive(true);
        if (animationCount != 0)
            animator.Play(animationName + Random.Range(1, animationCount + 1));
        else
        {
            animator.Play(animationName);
        }

    }

    private void StopAnimation()
    {
        active = false;
        gameObject.SetActive(false);
    }
}

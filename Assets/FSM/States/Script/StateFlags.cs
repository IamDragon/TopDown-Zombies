using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFlags : MonoBehaviour
{
    private Animator animator;

    public bool Active { get; set; }
    public bool Hit { get { return animator.GetBool("hit"); } }
    public bool Idling { get; set; }
    public bool FinnishedIdling { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Active = true;
        Idling = false; 
        FinnishedIdling = true;
    }


}

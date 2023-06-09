using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    public class SIdleForTime : State
{
    [SerializeField] private float idleTime;
    private Rigidbody rigidbody;
    private StateFlags flags;
    private Timer timer;

    public SIdleForTime(float idleTime)
    {
        this.idleTime = idleTime;
    }

    public override void Enter(Context contex)
    {
        base.Enter(contex);

        if (!rigidbody)
            rigidbody = contex.GetComponent<Rigidbody>();
        if(!flags)
            flags = contex.GetComponent<StateFlags>();
        if (timer == null)
            timer = new Timer(idleTime, false);

        rigidbody.velocity = Vector3.zero;

        flags.FinnishedIdling = false;
        flags.Idling= true;

        timer.ResetTimer();
    }



    public override void Exit(Context contex)
    {
        flags.Idling = false;
        timer.ResetTimer();
    }

    public override void UpdateState(Context contex)
    {
        if (timer.TickTimer(Time.deltaTime))
            FinishIdle();
    }

    private void FinishIdle()
    {
        flags.FinnishedIdling = true;
    }

}
}

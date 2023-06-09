using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : ScriptableObject
{
    protected StateManager stateManager;
    public virtual void Init(StateManager stateManager)
    {
        this.stateManager = stateManager;
        GetRelevantComponents();
    }
    public virtual void Enter() { }
    public virtual void Exit() { }
    protected virtual void GetRelevantComponents() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private State[] states;
    [SerializeField] private int defaultStateIndex;
    State currentState;

    private void Start()
    {
        currentState = states[defaultStateIndex];
        currentState.Enter();

        InitStates();
    }

    private void InitStates()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].Init(this);
        }
    }

    public void ChangeState(int index)
    {
        if (index < 0 || index >= states.Length)
        {
            currentState.Exit();
            currentState = states[defaultStateIndex];
            currentState.Enter();
            Debug.Log("index " + index + " was out of range, set current state to default");
        }
        else
        {
            currentState.Exit();
            currentState = states[index];
            currentState.Enter();
        }
        Debug.Log(transform.name + " has entered " + currentState.ToString() + " state");
    }
}

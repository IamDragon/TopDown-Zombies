using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{

    public class Context : MonoBehaviour
    {
        [SerializeField] protected List<State> states;
        [SerializeField] private State currentState;
        [SerializeField] protected int defaultState;
        private State goalState;
        private int goalID;

        protected virtual void Awake()
        {
            states = new List<State>();
        }
        protected virtual void Start()
        {
            for (int i = 0; i < states.Count; ++i)
                states[i].ID = i;

            if (currentState == null)
                currentState = states[defaultState];
            currentState.Enter(this);
        }

        protected virtual void Update()
        {
            UpdateMachine();
        }

        public void AddState(State state) { states.Add(state); }
        public void SetDefaultState(State state) { defaultState = state.ID; }

        public bool TransitionState(int goal)
        {
            if (goal >= states.Count)
                return false;
            goalState = states[goal];
            return true;
        }

        public void UpdateMachine()
        {
            if (states.Count == 0)
                return;
            if (currentState == null)
                currentState = states[defaultState];
            if (currentState == null)
                return;

            //update current state, and check for a transition
            int oldStateID = currentState.ID;
            goalID = CheckTransitions();

            //switch if there was a transition
            if (goalID != oldStateID)
            {
                if (TransitionState(goalID))
                {
                    currentState.Exit(this);
                    currentState = goalState;
                    currentState.Enter(this);
                }
            }
            currentState.UpdateState(this);
        }

        //Override in children class to define indepandant behaviours
        protected virtual int CheckTransitions()
        {
            return defaultState;
        }

    }
}

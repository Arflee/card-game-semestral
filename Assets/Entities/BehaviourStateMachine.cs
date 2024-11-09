using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourStateMachine : MonoBehaviour
{
    [SerializeField] private BasicBehaviorState[] states;
    private BasicBehaviorState currentState;
    float stateTimeLeft;

    void Start()
    {
        foreach (BasicBehaviorState state in states)
        {
            state.active.finnished = true;
            state.active.enabled = true;
        }
        SwitchState(states[0]);
    }

    public void SwitchState(BasicBehaviorState nextState)
    {
        if (currentState != null)
            currentState.active.enabled = false;
        currentState = nextState;
        currentState.active.enabled = true;
        stateTimeLeft = currentState.maxTime;
    }

    private void Update()
    {
        if (currentState.active.finnished || (stateTimeLeft < 0 && currentState.maxTime > 0))
        {
            SwitchState(currentState.next);
        }

        stateTimeLeft -= Time.deltaTime;
    }
}

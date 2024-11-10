using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedBehaviour : BehaviourState
{
    [SerializeField] private List<BehaviourState> states;

    [SerializeField] private BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var state in states)
        {
            state.enabled = true;
            state.CancelFinish += LinkedFinish;
        }
    }

    protected virtual void OnDisable()
    {
        foreach (var state in states)
        {
            state.CancelFinish -= LinkedFinish;
        }
    }

    private bool LinkedFinish()
    {
        foreach (var state in states)
        {
            state.enabled = false;
        }
        Finished();
        return true;
    }
}

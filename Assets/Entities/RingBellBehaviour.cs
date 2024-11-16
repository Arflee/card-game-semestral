using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBellBehaviour : BehaviourState
{
    [SerializeField] private Bell bell;
    [SerializeField] private BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bell.Ring();
    }

    private void Update()
    {
        Finished();
    }
}

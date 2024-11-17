using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBearBehaviour : BehaviourState
{
    [SerializeField] public BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var item in GetComponents<BehaviourState>())
            if (item != this)
                item.enabled = false;
    }

    private void Update()
    {
        Finished();
    }
}

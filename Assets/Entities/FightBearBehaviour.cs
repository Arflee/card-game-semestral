using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBearBehaviour : BehaviourState
{
    [SerializeField] public BehaviourState onWinNextState;
    [SerializeField] public BehaviourState onLooseNextState;

    public override BehaviourState NextState()
    {
        return onWinNextState;
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

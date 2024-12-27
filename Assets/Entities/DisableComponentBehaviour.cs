using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponentBehaviour : BehaviourState
{
    [SerializeField] private List<Behaviour> componentsToDisable;

    [Header("States")]
    [SerializeField] private BehaviourState nextState;

    protected override void OnEnable()
    {
        base.OnEnable();
        componentsToDisable.ForEach(c => c.enabled = false);
    }

    public override BehaviourState NextState()
    {
        throw new System.NotImplementedException();
    }
}

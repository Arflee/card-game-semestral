using System.Collections.Generic;
using UnityEngine;

public class EnableComponentBehaviour : BehaviourState
{
    [SerializeField] private List<Behaviour> componentsToEnable;

    [Header("States")]
    [SerializeField] private BehaviourState nextState;

    protected override void OnEnable()
    {
        base.OnEnable();
        componentsToEnable.ForEach(c => c.enabled = true);
    }

    public override BehaviourState NextState()
    {
        throw new System.NotImplementedException();
    }
}

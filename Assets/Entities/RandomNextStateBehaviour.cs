using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNextStateBehaviour : BehaviourState
{
    [SerializeField] private List<BehaviourState> nextStates;

    public override BehaviourState NextState()
    {
        return nextStates[Random.Range(0, nextStates.Count)];
    }

    private void Update()
    {
        Finished();
    }
}

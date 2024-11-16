using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBridgeBehaviour : BehaviourState
{
    public GameObject bridge;
    [SerializeField] BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bridge.SetActive(false);
    }

    private void Update()
    {
        Finished();
    }
}

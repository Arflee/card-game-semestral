using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjBehaviour : BehaviourState
{
    public BehaviourState nextState;
    public GameObject go;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Destroy(go);
    }

    private void Update()
    {
        Finished();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DisapearBehaviour : BehaviourState
{
    public override BehaviourState NextState()
    {
        return null;
    }

    protected virtual void Update()
    {
        gameObject.SetActive(false);
    }
}

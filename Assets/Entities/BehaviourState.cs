using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourState : MonoBehaviour
{
    public event Func<bool> CancelFinish;
    public abstract BehaviourState NextState();

    protected virtual void OnEnable()
    {
        Debug.Log("Enabled: " + ToString(), this);
    }

    public void Finished()
    {
        if (CancelFinish != null)
            if (CancelFinish.Invoke())
                return;
        enabled = false;
        BehaviourState next = NextState();
        if (next != null)
            next.enabled = true;
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

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
        if (CancelFinish?.Invoke() ?? false)
        {
            return;
        }

        enabled = false;
        BehaviourState next = NextState();
        if (next != null)
            next.enabled = true;
    }
}

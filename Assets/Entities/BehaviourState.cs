using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourState : MonoBehaviour
{
    public abstract BehaviourState NextState();

    protected virtual void OnEnable()
    {
        Debug.Log("Enabled: " + ToString(), this);
    }

    public void Finished()
    {
        enabled = false;
        BehaviourState next = NextState();
        if (next != null)
            next.enabled = true;
    }
}

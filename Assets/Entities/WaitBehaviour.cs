using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBehaviour : BehaviourState
{
    [Header("States")]
    public BehaviourState nextState;
    public float minTimeActive;
    public float maxTimeActive;

    protected float timeLeft;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        timeLeft = Random.Range(minTimeActive, maxTimeActive);
    }

    protected virtual void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
            Finished();
    }
}

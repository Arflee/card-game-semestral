using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitHidenBehaviour : BehaviourState
{
    public BehaviourState nextState;
    public int minTime;
    public int maxTime;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        gameObject.SetActive(false);
        float pause = Random.Range(minTime, maxTime);
        DOVirtual.DelayedCall(pause, () =>
        {
            gameObject.SetActive(true);
            Finished();
        });
    }
}

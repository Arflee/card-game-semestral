using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioBehaviour : BehaviourState
{
    [SerializeField] AudioSource source;
    [SerializeField] BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        source.Play();
    }

    private void Update()
    {
        Finished();
    }
}

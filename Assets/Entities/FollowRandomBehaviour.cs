using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRandomBehaviour : BehaviourState
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float distanceThreshold = 0.1f;

    [TagField] public string targetsTag;
    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    private Transform target;

    protected override void OnEnable()
    {
        base.OnEnable();
        var objs = GameObject.FindGameObjectsWithTag(targetsTag);
        target = objs[Random.Range(0, objs.Length)].transform   ;
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected virtual void Update()
    {
        if (target == null)
            return;

        Vector3 targetPoint = target.position;
        Vector2 targetDirection = targetPoint - transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            Finished();
            return;
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }

}

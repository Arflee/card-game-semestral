using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointBehaviour : PointBehaviour
{
    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    private int targetPointIndex = 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        targetPointIndex = Random.Range(0, points.Length);
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected virtual void Update()
    {
        if (points.Length == 0)
            return;

        Vector2 targetPoint = points[targetPointIndex];
        Vector2 targetDirection = targetPoint - (Vector2)transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            Finished();
            return;
        }

        transform.Translate(speed * Time.deltaTime * targetDirection.normalized);
    }
}

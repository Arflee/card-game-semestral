using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestPointBehaviour : PointBehaviour
{
    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected virtual void Update()
    {
        if (points.Length == 0)
            return;

        Vector2 targetPoint = points[0];
        float closestDist = float.MaxValue;
        foreach (var p in points)
        {
            float dist = Vector2.Distance(p, transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                targetPoint = p;
            }
        }

        Vector2 targetDirection = targetPoint - (Vector2)transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            Finished();
            return;
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class RandomVisiblePointBehaviour : PointBehaviour
{
    [Header("Other")]
    [SerializeField] public BehaviourState nextState;
    [SerializeField] private LayerMask obstacleLayers;

    private Vector2 targetPoint;

    protected override void OnEnable()
    {
        base.OnEnable();
        var visiblePoints = new List<Vector2>();
        foreach (var p in points)
        {
            Vector2 dir = p - (Vector2)transform.position;
            float dist = dir.magnitude;
            if (!Physics2D.Raycast(transform.position, dir, dist, obstacleLayers))
                visiblePoints.Add(p);
        }

        visiblePoints.Remove(targetPoint);
        if (visiblePoints.Count == 0)
        {
            targetPoint = transform.position;
            return;
        }

        Vector2 newPoint = visiblePoints[Random.Range(0, visiblePoints.Count)];
        targetPoint = newPoint;
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected virtual void Update()
    {
        if (points.Length == 0)
            return;

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

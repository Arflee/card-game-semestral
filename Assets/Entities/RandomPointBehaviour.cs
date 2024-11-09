using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPointBehaviour : BehaviourState
{
    [SerializeField] float speed = 5f;
    [SerializeField] float distanceThreshold = 0.1f;

    public Vector2[] points = new Vector2[0];

    [Header("States")]
    [SerializeField] public BehaviourState nextState;

    [Header("Gizmos")]
    public Color gizmosColor = Color.blue;

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

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehaviour : BehaviourState
{
    [SerializeField] float speed = 5f;
    [SerializeField] float distanceThreshold = 0.1f;
    [SerializeField] float pause = 0f;

    public enum Mode { Normal, Loop, Once, RandomWalk }
    public Mode mode = Mode.Normal;
    public Vector2[] points = new Vector2[0];

    [Header("States")]
    public BehaviourState nextState;
    public float maxTimeActive;

    [Header("Gizmos")]
    public Color gizmosColor = Color.blue;

    private int targetPointIndex = 0;
    private int prevPointIndex = 0;
    private int indexDirection = 1;
    private bool isWaiting = false;
    protected float timeLeft;

    protected virtual void OnEnable()
    {
        Debug.Log("Enabled: " + ToString(), this);
        targetPointIndex = 0;
        prevPointIndex = 0;
        indexDirection = 1;
        isWaiting = false;
        Finished = false;
        timeLeft = maxTimeActive;
    }

    protected virtual void Update()
    {
        if (points.Length == 0)
            return;

        if (maxTimeActive > 0)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
                Finished = true;
        }

        Vector2 targetPoint = points[targetPointIndex];
        Vector2 targetDirection = targetPoint - (Vector2)transform.position;
        float distance = targetDirection.magnitude;

        if (distance <= distanceThreshold && !isWaiting)
        {
            if (points.Length == 1)
                return;

            isWaiting = true;
            if (mode == Mode.Once && targetPointIndex == points.Length - 1)
            {
                Finished = true;
                return;
            }
            DOVirtual.DelayedCall(pause, () =>
            {
                if (points.Length == 2)
                {
                    targetPointIndex = 1 - targetPointIndex;
                    isWaiting = false;
                    return;
                }

                if (mode == Mode.RandomWalk)
                {
                    int newIndex = Random.Range(0, points.Length);
                    while (newIndex == targetPointIndex || newIndex == prevPointIndex)
                        newIndex = Random.Range(0, points.Length);
                    prevPointIndex = targetPointIndex;
                    targetPointIndex = newIndex;
                    isWaiting = false;
                    return;
                }

                targetPointIndex += indexDirection;
                if (mode == Mode.Loop && targetPointIndex == points.Length)
                    targetPointIndex = 0;
                else if (targetPointIndex == points.Length || targetPointIndex < 0)
                {
                    indexDirection *= -1;
                    targetPointIndex += indexDirection * 2;
                }
                isWaiting = false;
            });
        }

        if (!isWaiting)
            transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}

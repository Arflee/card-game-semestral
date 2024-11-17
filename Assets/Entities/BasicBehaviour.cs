using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehaviour : PointBehaviour
{
    [Header("Rest")]
    [SerializeField] float pause = 0f;

    public enum Mode { Normal, Loop, Once, RandomWalk }
    public Mode mode = Mode.Normal;

    public LayerMask obstacles;

    [Header("States")]
    public BehaviourState nextState;
    public BehaviourState whenPathBlockedState;

    private int targetPointIndex = 0;
    private int prevPointIndex = 0;
    private int indexDirection = 1;
    private bool isWaiting = false;
    private bool pathBlocked = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        targetPointIndex = 0;
        prevPointIndex = 0;
        indexDirection = 1;
        isWaiting = false;
        pathBlocked = false;
    }

    protected virtual void Update()
    {
        if (points.Length == 0)
            return;

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
                Finished();
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

                if (obstacles.value != 0)
                {
                    Vector2 dir = points[targetPointIndex] - (Vector2)transform.position;
                    if (Physics2D.Raycast(transform.position, dir, dir.magnitude, obstacles))
                    {
                        pathBlocked = true;
                        Finished();
                    }
                }
            });
        }

        if (!isWaiting)
            transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }

    public override BehaviourState NextState()
    {
        if (pathBlocked)
            return whenPathBlockedState;
        return nextState;
    }
}

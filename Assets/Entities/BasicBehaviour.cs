using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float distanceThreshold = 0.1f;
    [SerializeField] float pause = 0f;

    public enum Mode { Normal, Loop, Once }
    public Mode mode = Mode.Normal;
    public Vector2[] points = new Vector2[0];

    private int targetPointIndex = 0;
    private int indexDirection = 1;
    private bool isWaiting = false;

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
                return;
            DOVirtual.DelayedCall(pause, () =>
            {
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBehaviour : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float distanceThreshold = 0.1f;

    public Vector2[] points = new Vector2[0];
    public bool loop;

    private int targetPointIndex = 0;
    private int indexDirection = 1;
    
    private void Start()
    {
        if (points.Length == 0)
            points = new Vector2[] { transform.position };
    }

    private void Update()
    {
        Vector2 targetPoint = points[targetPointIndex];
        Vector2 targetDirection = targetPoint - (Vector2)transform.position;
        float distance = targetDirection.magnitude;

        if (distance <= distanceThreshold)
        {
            targetPointIndex += indexDirection;
            if (loop && targetPointIndex == points.Length)
                targetPointIndex = 0;
            else if (targetPointIndex == points.Length || targetPointIndex < 0)
            {
                indexDirection *= -1;
                targetPointIndex += indexDirection * 2;
            }
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }
}

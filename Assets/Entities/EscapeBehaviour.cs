using Pospec.Helper.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBehaviour : PointBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private Sound screamSound;
    [SerializeField] private AudioSource source;

    public override BehaviourState NextState()
    {
        return null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var item in GetComponents<BehaviourState>())
            if (item != this)
                item.enabled = false;
        source.PlaySound(screamSound);
    }

    private void Update()
    {
        if (points.Length == 0)
            return;

        Vector2 targetPoint = points[0];
        float closestDist = float.MaxValue;
        foreach (var p in points)
        {
            Vector2 dir = p - (Vector2)transform.position;
            float dist = dir.magnitude;
            if (dist < closestDist && !Physics2D.Raycast(transform.position, dir, dist, obstacleLayers))
            {
                closestDist = dist;
                targetPoint = p;
            }
        }
        if (closestDist == float.MaxValue)
            return;

        Vector2 targetDirection = targetPoint - (Vector2)transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            gameObject.SetActive(false);
            Finished();
            return;
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }
}

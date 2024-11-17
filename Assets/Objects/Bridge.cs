using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private LayerMask bearLayer;
    public Vector2[] points;
    public LayerMask obstacleLayers;
    
    private DestroyBridgeBehaviour[] villagers = new DestroyBridgeBehaviour[0];

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            villagers = FindObjectsByType<DestroyBridgeBehaviour>(FindObjectsSortMode.None);
        }
    }

    private void Update()
    {
        foreach (var item in villagers)
        {
            Vector2 pos = item.transform.position;

            if (item.enabled)
                continue;

            Vector2 target = Vector2.zero;
            float minDir = float.PositiveInfinity;
            foreach (var p in points)
            {
                Vector2 dir = p - pos; // A -> B = B - A
                float dist = dir.magnitude;
                if (Physics2D.Raycast(pos, dir, dist, obstacleLayers))
                    continue;

                if (dist < minDir)
                {
                    minDir = dist;
                    target = p;
                }
            }

            if (minDir != float.PositiveInfinity)
            {
                item.enabled = true;
                item.Setup(target, this);
            }
        }
    }

    public void Colapase()
    {
        gameObject.SetActive(false);
        foreach (var item in villagers)
            item.BridgeWasDestroyed();
    }
}

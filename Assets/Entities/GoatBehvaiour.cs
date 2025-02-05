using DG.Tweening;
using Pospec.Helper.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatBehvaiour : BehaviourState
{
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float distanceThreshold = 0.1f;
    [SerializeField] float eatTime = 0f;
    [SerializeField] float wheatDestroyed = 0.9f;

    [SerializeField] private Field field;
    [SerializeField] BehaviourState nextState;
    [SerializeField] private Sound eatingSound;
    [SerializeField] private AudioSource source;

    bool isWaiting = false;
    EatabeWheat target = null;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    private void SearchNextTarget()
    {
        isWaiting = false;
        float closestDist = float.MaxValue;
        foreach (var w in field.Wheat)
        {
            if (w == null || w.Freshness < 0.9f)
                continue;
            float dist = Vector2.Distance(w.transform.position, transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                target = w;
            }
        }
    }

    private void Update()
    {
        if (field.WheatDestroyed > wheatDestroyed)
            Finished();

        if (isWaiting)
            return;

        if (target == null)
            SearchNextTarget();

        if (target == null)
            return;

        Vector2 targetDirection = target.transform.position - transform.position;
        float distance = targetDirection.magnitude;
        if (distance <= distanceThreshold)
        {
            isWaiting = true;
            DOVirtual.DelayedCall(eatTime, () =>
            {
                source.PlaySound(eatingSound);
                if (target != null)
                    target.Eat();
                SearchNextTarget();
            });
            return;
        }

        transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
    }
}

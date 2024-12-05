using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
public class LifeCrystalPanel : MonoBehaviour
{
    [SerializeField] private LifeCrystalParameters crystalParameters;
    [SerializeField] private LifeCrystal lifeCrystalPrefab;

    private Queue<LifeCrystal> spawnedCrystals = new();

    private void Start()
    {
        for (int i = 0; i < crystalParameters.CrystalAmount; i++)
        {
            var createdCrystal = Instantiate(lifeCrystalPrefab, transform);
            createdCrystal.Initialize(crystalParameters.Health);
            spawnedCrystals.Enqueue(createdCrystal);
        }
    }

    public bool TryAttackCrystal(int damage)
    {
        var lastCrystal = spawnedCrystals.Peek();
        if (lastCrystal == null)
            return false;


        if (lastCrystal.Health <= damage)
        {
            spawnedCrystals.Dequeue();
            lastCrystal.gameObject.SetActive(false);
            return spawnedCrystals.Count > 0;
        }

        lastCrystal.TakeDamage(damage);
        return true;
    }
}

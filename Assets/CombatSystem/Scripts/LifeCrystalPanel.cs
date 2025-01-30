using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCrystalPanel : MonoBehaviour
{
    [SerializeField] private LifeCrystal lifeCrystalPrefab;

    private Queue<LifeCrystal> spawnedCrystals = new();
    public int Amount => spawnedCrystals.Count;

    public void Initialize(LifeCrystalParameters crystalParameters)
    {
        for (int i = 0; i < crystalParameters.CrystalAmount; i++)
        {
            var createdCrystal = Instantiate(lifeCrystalPrefab, transform);
            createdCrystal.Initialize(crystalParameters.Health);
            spawnedCrystals.Enqueue(createdCrystal);
        }
    }

    public int AttackCrystal(int damage)
    {
        if (spawnedCrystals.Count == 0)
            return 0;

        var lastCrystal = spawnedCrystals.Peek();
        if (lastCrystal == null)
        {
            spawnedCrystals.Dequeue();
            return 0;
        }

        if (lastCrystal.Health <= damage)
        {
            spawnedCrystals.Dequeue();
            lastCrystal.gameObject.SetActive(false);
            return spawnedCrystals.Count;
        }

        lastCrystal.TakeDamage(damage);
        return spawnedCrystals.Count;
    }
}

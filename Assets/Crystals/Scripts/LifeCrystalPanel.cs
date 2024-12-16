using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutGroup))]
public class LifeCrystalPanel : MonoBehaviour
{
    [SerializeField] private CommonLifeCrystal lifeCrystalPrefab;

    private Queue<CommonLifeCrystal> spawnedCrystals = new();

    private void Start()
    {
        for (int i = 0; i < lifeCrystalPrefab.CrystalAmount; i++)
        {
            var createdCrystal = Instantiate(lifeCrystalPrefab, transform);
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

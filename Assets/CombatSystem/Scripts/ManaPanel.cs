using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManaPanel : MonoBehaviour
{
    [SerializeField] private GameObject manaCrystalPrefab;
    private List<GameObject> manaCrystals = new();

    public void SpawnCrystalPrefabs(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var crystal = Instantiate(manaCrystalPrefab, transform);
            crystal.SetActive(false);
            manaCrystals.Add(crystal);
        }
    }

    public void UseManaCrystals(int amount)
    {
        var activeCrystals = manaCrystals.Where(c => c.activeInHierarchy).Take(amount);

        foreach (var crystal in activeCrystals)
        {
            crystal.SetActive(false);
        }
    }

    public void ResetManaCrystals(int newCrystalAmount)
    {
        manaCrystals.ForEach(c => c.SetActive(false));

        for (int i = 0; i < newCrystalAmount; i++)
        {
            manaCrystals[i].SetActive(true);
        }
    }
}

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
            manaCrystals.Add(crystal);
        }

        //manaCrystals.Reverse();
    }

    public void UseManaCrystals(int amount)
    {
        var usedCrystals = manaCrystals.Take(amount);
        foreach (var crystal in usedCrystals)
        {
            crystal.SetActive(false);
        }
    }

    public void ResetManaCrystals(int newCrystalAmount)
    {
        for (int i = 0; i < newCrystalAmount; i++)
        {
            manaCrystals[i].SetActive(true);
        }

        for (int i = newCrystalAmount; i < manaCrystals.Count; i++)
        {
            manaCrystals[i].SetActive(false);
        }
    }
}

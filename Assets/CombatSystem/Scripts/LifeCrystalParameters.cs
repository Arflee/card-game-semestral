using UnityEngine;

[CreateAssetMenu(fileName = "LifeCrystalParameters", menuName = "New Life Crystal")]

public class LifeCrystalParameters : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int crystalAmount;

    public int Health => health;
    public int CrystalAmount => crystalAmount;

    public LifeCrystalParameters(int health, int crystalAmount)
    {
        this.health = health;
        this.crystalAmount = crystalAmount;
    }
}

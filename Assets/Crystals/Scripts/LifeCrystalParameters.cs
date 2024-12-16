using UnityEngine;

[CreateAssetMenu(fileName = "LifeCrystalParameters", menuName = "New Life Crystal")]

public class LifeCrystalParameters : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int crystalAmount;

    public int Health => health;
    public int CrystalAmount => crystalAmount;
}

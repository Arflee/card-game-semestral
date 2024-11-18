using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatCard", menuName = "New Combat Card")]
public class CombatCard : ScriptableObject
{
    [Header("Card parameters")]
    [SerializeField, Min(1)] private int health;
    [SerializeField, Min(0)] private int damage;
    [SerializeField, Min(0)] private int manaCost;
    [SerializeField] private CardEffect[] effects;

    [Header("Card information")]
    [SerializeField] private string cardName;
    [SerializeField, TextArea(3, 7)] private string cardDescription;
    [SerializeField] private Sprite cardSprite;

    public string Name => cardName;
    public string Description => cardDescription;
    public int Health => health;
    public int Damage => damage;
    public int ManaCost => manaCost;
    public IEnumerable<CardEffect> CardEffects => effects;
}

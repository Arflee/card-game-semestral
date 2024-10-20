using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatCard", menuName = "New Combat Card Type")]
public class CombatCard : ScriptableObject
{
    [Flags]
    public enum EffectType
    {
        Haste = 1,
        Deathtouch = 2,
        Attacker = 4,
        Blocker = 8
    }

    [Header("Card parameters")]
    [SerializeField, Min(1)] private int health;
    [SerializeField, Min(0)] private int damage;
    [SerializeField, EnumFlags] private EffectType cardEffect;

    [Header("Card information")]
    [SerializeField] private string cardName;
    [SerializeField, TextArea(3, 7)] private string cardDescription;
    [SerializeField] private Sprite cardSprite;

    public string CardName => cardName;
    public string CardDescription => cardDescription;
    public int Health => health;
    public int Damage => damage;
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatCard", menuName = "New Card/Combat Card")]
public class CombatCard : CardCharsSO
{
    [Header("Card parameters")]
    [SerializeField, Min(1)] private int health;
    [SerializeField, Min(0)] private int damage;
    [SerializeField] private CardEffect[] onUseEffects;
    [SerializeField] private CardEffect[] onStartTurnEffects;
    [SerializeField] private CardEffect[] onDeathEffects;
    [SerializeField] private CardEffect[] afterTurnEffects;

    public int Health => health;
    public int Damage => damage;
    public IEnumerable<CardEffect> OnUseEffects => onUseEffects;
    public IEnumerable<CardEffect> OnStartTurnEffects => onStartTurnEffects;
    public IEnumerable<CardEffect> OnDeathEffects => onDeathEffects;
    public IEnumerable<CardEffect> AfterTrunEffects => afterTurnEffects;

}

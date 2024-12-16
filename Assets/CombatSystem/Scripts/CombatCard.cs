using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatCard", menuName = "New Card/Combat Card")]
public class CombatCard : CardCharsSO
{
    [Header("Card parameters")]
    [SerializeField, Min(1)] private int health;
    [SerializeField, Min(0)] private int damage;
    [SerializeField] private CardEffect[] combatEffects;
    [SerializeField] private NormalCardEffect[] onUseEffects;
    [SerializeField] private NormalCardEffect[] onStartTurnEffects;
    [SerializeField] private NormalCardEffect[] onDeathEffects;

    public int Health => health;
    public int Damage => damage;
    public IEnumerable<CardEffect> CardEffects => combatEffects;
    public IEnumerable<NormalCardEffect> OnUseEffects => onUseEffects;
    public IEnumerable<NormalCardEffect> OnStartTurnEffects => onStartTurnEffects;
    public IEnumerable<NormalCardEffect> OnDeathEffects => onDeathEffects;

}

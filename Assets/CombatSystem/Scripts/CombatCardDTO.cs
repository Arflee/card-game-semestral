using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatCardDTO
{
    public CombatCardDTO(CombatCard card)
    {
        Name = card.Name;
        Description = card.Description;
        Sprite = card.CardSprite;
        Health = card.Health;
        Damage = card.Damage;
        ManaCost = card.ManaCost;
        OnUseEffects = new(card.OnUseEffects ?? Enumerable.Empty<CardEffect>());
        OnStartTurnEffects = new(card.OnStartTurnEffects ?? Enumerable.Empty<CardEffect>());
        OnDeathEffects = new(card.OnDeathEffects ?? Enumerable.Empty<CardEffect>());
        AfterTurnEffect = new(card.AfterTrunEffects ?? Enumerable.Empty<CardEffect>());
        CardPrefab = card;
    }

    public string Name { get; }
    public string Description { get; }
    public Sprite Sprite { get; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; }
    public bool IsAlive => Health > 0;
    public List<CardEffect> OnUseEffects { get; private set; }
    public List<CardEffect> OnStartTurnEffects { get; private set; }
    public List<CardEffect> OnDeathEffects { get; private set; }
    public List<CardEffect> AfterTurnEffect { get; private set; }
    public CombatCard CardPrefab { get; private set; }
}

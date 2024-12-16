using System.Collections.Generic;
using System.Linq;

public class CombatCardDTO
{
    public CombatCardDTO(CombatCard card)
    {
        Name = card.Name;
        Description = card.Description;
        Health = card.Health;
        Damage = card.Damage;
        ManaCost = card.ManaCost;
        // CardEffects = new(card.CardEffects ?? Enumerable.Empty<CardEffect>());
        OnUseEffects = new(card.OnUseEffects ?? Enumerable.Empty<NormalCardEffect>());
        OnStartTurnEffects = new(card.OnStartTurnEffects ?? Enumerable.Empty<NormalCardEffect>());
        OnDeathEffects = new(card.OnDeathEffects ?? Enumerable.Empty<NormalCardEffect>());
    }

    public string Name { get; }
    public string Description { get; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; }
    public bool IsAlive => Health > 0;
    // public List<CardEffect> CardEffects { get; private set; }
    public List<NormalCardEffect> OnUseEffects { get; private set; }
    public List<NormalCardEffect> OnStartTurnEffects { get; private set; }
    public List<NormalCardEffect> OnDeathEffects { get; private set; }
}

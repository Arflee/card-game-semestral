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
        CardEffects = new(card.CardEffects ?? Enumerable.Empty<CardEffect>());
    }

    public string Name { get; }
    public string Description { get; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int ManaCost { get; }
    public bool IsAlive => Health > 0;
    public List<CardEffect> CardEffects { get; private set; }
}

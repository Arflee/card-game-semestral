using System.Collections.Generic;

public class CombatCardDTO
{
    public CombatCardDTO(CombatCard card)
    {
        Name = card.Name;
        Description = card.Description;
        Health = card.Health;
        Damage = card.Damage;
        CardEffects = new(card.CardEffects);
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsAlive => Health > 0;
    public List<CardEffect> CardEffects { get; private set; }
}

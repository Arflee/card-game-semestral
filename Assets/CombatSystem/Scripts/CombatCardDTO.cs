using System;

public class CombatCardDTO
{
    public CombatCardDTO(string name, string description, int health, int damage)
    {
        Name = name;
        Description = description;
        Health = health;
        Damage = damage;
    }

    public CombatCardDTO(CombatCard card)
    {
        Name = card.CardName;
        Description = card.CardDescription;
        Health = card.Health;
        Damage = card.Damage;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsAlive => Health > 0;

    public event Action<CombatCardDTO> OnTakeDamageEvent;

    public void TakeDamage(CombatCardDTO source)
    {
        Health -= source.Damage;
        OnTakeDamageEvent(this);
    }
}

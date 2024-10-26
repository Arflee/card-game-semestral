using System;
using System.Xml.Linq;
using UnityEngine;

public class CombatCardDTO
{
    public CombatCardDTO(string name, string description, int health, int damage)
    {
        Name = name;
        Description = description;
        Health = health;
        Damage = damage;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsAlive => Health > 0;
}

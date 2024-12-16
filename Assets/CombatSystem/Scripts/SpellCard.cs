using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellCard", menuName = "New Card/Spell Card")]
public class SpellCard : CardCharsSO
{
    [SerializeField] private CardEffect[] effects;

    public IEnumerable<CardEffect> SpellEffects => effects;
}

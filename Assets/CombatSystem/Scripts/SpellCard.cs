using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpellCard", menuName = "New Card/Spell Card")]
public class SpellCard : CardCharsSO
{
    [SerializeField] private SpellEffect[] effects;

    public IEnumerable<SpellEffect> SpellEffects => effects;
}

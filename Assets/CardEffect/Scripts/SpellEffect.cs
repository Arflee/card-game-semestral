using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : ScriptableObject
{
    public abstract void OnUse(Card targetCard, List<Card> playerTable);
}

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All On Use", menuName = "Card Effects/On Use/Destroy All")]
public class DestroyAllOnUse : CardEffect
{
    public override void OnUse(CombatState combatState, CombatStateMachine manager, Card card)
    {
        var cards = new List<Card>(combatState.OwnersCardsOnTable);
        cards.RemoveAt(cards.Count - 1);
        cards.AddRange(combatState.OpponentsCardsOnTable);

        foreach (var c in cards)
        {
            manager.DestroyCard(c);
        }
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All On Use", menuName = "Card Effects/On Use/Destroy All")]
public class DestroyAllOnUse : CardEffect
{
    public override void OnUse(CombatState combatState, CombatStateMachine manager, Card card)
    {
        while (combatState.OwnersCardsOnTable.Count > 1)
        {
            manager.DestroyCard(combatState.OwnersCardsOnTable[0]);
        }

        while (combatState.OpponentsCardsOnTable.Count > 0)
        {
            manager.DestroyCard(combatState.OpponentsCardsOnTable[0]);
        }
    }
}

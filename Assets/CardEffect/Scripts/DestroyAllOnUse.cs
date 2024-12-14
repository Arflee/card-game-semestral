using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All On Use", menuName = "Card Effects/On Use/Destroy All")]
public class DestroyAllOnUse : CardEffect
{
    public override void OnUse(CombatState combatState, CombatStateMachine manager)
    {
        while (combatState.OwnersCardsOnTable.Count > 1)
        {
            manager.RemoveCardFromTable(combatState.OwnersCardsOnTable[0]);
            combatState.OwnersCardsOnTable.RemoveAt(0);
        }

        foreach (var card in combatState.OpponentsCardsOnTable)
        {
            manager.RemoveCardFromTable(card);
        }

        combatState.OpponentsCardsOnTable.Clear();
    }
}

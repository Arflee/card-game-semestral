using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All and Buff Yourself On Use", menuName = "Card Effects/On Use/Destroy All and Buff Yourself")]
public class DestroyAllAndBuffYourselfOnUseEffect : CardEffect
{
    [SerializeField] private int healthBuffPerCard = 1;
    [SerializeField] private int damageBuffPerCard = 1;

    public override void OnUse(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        var cards = new List<Card>(cardOwner.OwnersCardsOnTable);
        cards.RemoveAt(cards.Count - 1);

        foreach (var c in cards)
        {
            manager.DestroyCard(c);
            card.BuffDamage(damageBuffPerCard);
            card.BuffHealth(healthBuffPerCard);
        }
    }
}

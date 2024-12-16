using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy Your Cards And Buff Yourself", menuName = "Card Effects/Destroy Your Cards And Buff Yourself")]
public class DestroyYourCardsAndBuffYourselfEffect : CardEffect
{
    [SerializeField] private int healthBuffPerCard = 1;
    [SerializeField] private int damageBuffPerCard = 1;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        var cards = new List<Card>(cardOwner.OwnersCardsOnTable);

        foreach (var c in cards)
        {
            if (c == card)
                continue;
            yield return manager.DestroyCard(c);
            card.BuffDamage(damageBuffPerCard);
            card.BuffHealth(healthBuffPerCard);
        }
    }
}

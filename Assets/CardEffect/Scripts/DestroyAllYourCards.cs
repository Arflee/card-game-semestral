using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All Your Cards", menuName = "Card Effects/Destroy All Your Cards")]
public class DestroyAllYourCards : CardEffect
{
    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        var cards = new List<Card>(cardOwner.OwnersCardsOnTable);

        foreach (var c in cards)
        {
            if (c == card)
                continue;
            yield return manager.DestroyCard(c);
        }
    }
}

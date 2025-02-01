using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform Your Cards", menuName = "Card Effects/Transform Your Cards")]
public class TransformYourCardsEffect : CardEffect
{
    [SerializeField] private CombatCard _changeCard;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var c in cardOwner.OwnersCardsOnTable)
        {
            if (c == card)
                continue;
            c.Reinitialize(_changeCard);
            c.TurnPlayed = manager.CurrentTurn;
            foreach (var eff in c.CombatDTO.OnUseEffects)
            {
                yield return eff.StartEffect(manager, c);
            }
        }
    }
}

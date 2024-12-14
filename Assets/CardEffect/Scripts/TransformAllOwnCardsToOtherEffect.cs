using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Transform all own cards")]
public class TransformAllOwnCardsToOtherEffect : CardEffect
{
    [SerializeField] private CombatCard changeCard;

    public override void OnUse(CombatState combatState, CombatStateMachine manager, Card card)
    {
        foreach (var c in combatState.OwnersCardsOnTable)
        {
            c.Reinitialize(changeCard);
        }
    }
}

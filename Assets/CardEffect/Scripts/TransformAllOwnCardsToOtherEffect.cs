using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Transform all own cards")]
public class TransformAllOwnCardsToOtherEffect : CardEffect
{
    [SerializeField] private CombatCard changeCard;

    public override void OnUse(CombatState combatState)
    {
        foreach (var card in combatState.OwnersCardsOnTable)
        {
            card.Reinitialize(changeCard);
        }
    }
}

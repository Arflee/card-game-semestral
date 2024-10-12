using UnityEngine;

public class CombatSlot : MonoBehaviour
{
    [SerializeField] private CombatSlot oppositeSlot;

    private CombatCard _cardInSlot;

    public void PutCardInSlot(CombatCard card)
    {
        _cardInSlot = card;
        Debug.Log(card.CardName);
    }
}

using UnityEngine;

public class CombatSlot : MonoBehaviour
{
    [SerializeField] private CombatSlot oppositeSlot;

    public CombatCard CardInSlot { get; private set; }

    public CombatSlot OppositeSlot => oppositeSlot;

    public void PutCardInSlot(CombatCard card)
    {
        CardInSlot = card;
    }

}

using UnityEngine;

public class CombatSlot : MonoBehaviour
{
    [SerializeField] private CombatSlot oppositeSlot;

    public CombatCardDTO CardInSlot { get; private set; }

    public CombatSlot OppositeSlot => oppositeSlot;

    public void PutCardInSlot(CombatCardDTO card)
    {
        CardInSlot = card;
    }

}

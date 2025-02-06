using UnityEngine;
using UnityEngine.UI;

public class RewardPanel : MonoBehaviour
{
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button deckEditButton;

    public Button PanelButton => deckEditButton;

    public void SetRewardCard(CombatCard[] combatCards)
    {
        foreach (var combatCard in combatCards)
        {
            var cardSlot = Instantiate(cardSlotPrefab, panel.transform);
            var card = cardSlot.GetComponentInChildren<Card>();
            card.Initialize(combatCard, null, panel.transform.position);

            card.CardVisual.LocalCanvas.overrideSorting = true;
            card.CardVisual.LocalCanvas.sortingLayerName = "Foreground";
            card.CardVisual.LocalCanvas.sortingOrder = 1;
        }
    }
}

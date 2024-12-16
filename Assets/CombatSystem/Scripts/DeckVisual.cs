using TMPro;
using UnityEngine;

public class DeckVisual : MonoBehaviour
{
    [SerializeField] private CombatStateMachine combatManager;
    [SerializeField] private TextMeshProUGUI cardCountTMP;

    private void Update()
    {
        int cardCount = combatManager.PlayerDeck.CardCount();
        if (cardCount == 0)
            gameObject.SetActive(false);

        cardCountTMP.text = cardCount.ToString();
    }
}

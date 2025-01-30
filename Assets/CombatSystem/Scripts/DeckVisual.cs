using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckVisual : MonoBehaviour
{
    [SerializeField] private CombatStateMachine combatManager;
    [SerializeField] private TextMeshProUGUI cardCountTMP;
    [SerializeField] private Shadow shadow;
    [SerializeField] private float maxShadow = 10;
    [SerializeField] private int maxCardsForShadow = 20;

    private void Update()
    {
        int cardCount = combatManager.PlayerDeck.CardCount();
        if (cardCount == 0)
            gameObject.SetActive(false);

        cardCountTMP.text = cardCount.ToString();
        shadow.effectDistance = -Vector2.one * Mathf.Clamp(cardCount * (maxShadow /  maxCardsForShadow), 0, maxShadow);
    }
}

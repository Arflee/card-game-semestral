using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardNameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private Button button;

    public void Initialize(CombatCard card, DeckEditorManger manager, int pos)
    {
        cardName.text = card.Name;
        manaText.text = card.ManaCost.ToString();
        button.onClick.AddListener(() =>
        {
            manager.RemoveFromDeck(pos);
            Destroy(gameObject);
        });
    }
}

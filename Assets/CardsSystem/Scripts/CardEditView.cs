using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardEditView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private Image template;
    [SerializeField] private Image image;
    [SerializeField] private Button button;

    GameObject inDeck;

    private Color disabledColor;
    private Color imageColor;
    private Color templateColor;
    private Color nameColor;
    private Color describtionColor;
    private Color damageColor;
    private Color healthColor;
    private Color manaColor;

    public void Initialize(CombatCard card, int pos, DeckEditorManger manager)
    {
        cardName.text = card.Name;
        cardDescription.text = card.Description;
        damageText.text = card.Damage.ToString();
        healthText.text = card.Health.ToString();
        manaText.text = card.ManaCost.ToString();
        if (card.CardSprite != null)
            image.sprite = card.CardSprite;

        disabledColor = button.colors.disabledColor;
        imageColor = image.color;
        templateColor = template.color;
        nameColor = cardName.color;
        describtionColor = cardDescription.color;
        damageColor = damageText.color;
        healthColor = healthText.color;
        manaColor = manaText.color;
        EnableCard();

        button.onClick.AddListener(() =>
        {
            if (inDeck)
            {
                manager.RemoveFromDeck(pos);
                Destroy(inDeck);
                inDeck = null;
            }
            else
            {
                DisableCard(manager.AddToDeck(pos));
            }
        });
    }

    public void DisableCard(GameObject view)
    {
        inDeck = view;
        image.color = imageColor;
        template.color = templateColor;
        cardName.color = nameColor;
        cardDescription.color = describtionColor;
        damageText.color = damageColor;
        healthText.color = healthColor;
        manaText.color = manaColor;
    }

    public void EnableCard()
    {
        image.color *= disabledColor;
        template.color *= disabledColor;
        cardName.color *= disabledColor;
        cardDescription.color *= disabledColor;
        damageText.color *= disabledColor;
        healthText.color *= disabledColor;
        manaText.color *= disabledColor;
    }
}

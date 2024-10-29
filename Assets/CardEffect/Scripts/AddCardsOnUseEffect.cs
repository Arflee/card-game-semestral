using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "CardEffects/On Use/Add cards to player hand on play")]
public class AddCardsOnUseEffect : CardEffect
{
    [SerializeField, Min(1)] private int cardsAmount;

    public override void OnUse(CardDeck deck, Card target)
    {
        for (int i = 0; i < cardsAmount; i++)
        {
            if (deck.TakeCard() == null)
            {
                Debug.Log("ran out of cards in deck");
            }
        }
    }
}

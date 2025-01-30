using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeckVisual : MonoBehaviour
{
    [SerializeField] private EnemyInitializer initializer;
    [SerializeField] private Shadow shadow;
    [SerializeField] private float maxShadow = 10;
    [SerializeField] private int maxCardsForShadow = 20;

    private void Update()
    {
        int cardCount = initializer.CardCount();
        if (cardCount == 0)
            gameObject.SetActive(false);

        shadow.effectDistance = -Vector2.one * Mathf.Clamp(cardCount * (maxShadow / maxCardsForShadow), 0, maxShadow);
    }
}

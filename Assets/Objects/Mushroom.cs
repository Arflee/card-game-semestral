using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    bool close = false;
    [SerializeField] private LayerMask bearLayer;
    [SerializeField] private CombatCard card;

    private void OnValidate()
    {
        if (card == null)
            return;
        name = "mushroom " + card.name;
    }

    private void Eat()
    {
        var playersDeck = FindObjectOfType<CardDeck>();
        playersDeck.AddNewCard(card);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && close)
            Eat();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            close = true;
            InteractText.Instance.Use(this, "snìz");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            close = false;
            InteractText.Instance.Disable(this);
        }
    }

}

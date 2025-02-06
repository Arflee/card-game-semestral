using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class OpenDeckEditor : MonoBehaviour
{
    [SerializeField] private DeckEditorManger deckEditorPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    private CardDeck deck;

    private void Start()
    {
        deck = FindObjectOfType<CardDeck>();
    }

    private void Update()
    {
        if (deck == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (deck.AllCardsCount() < deckEditorPanel.MinCards || CombatStateMachine.GameActive)
        {
            infoText.gameObject.SetActive(false);
            return;
        }

        infoText.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.I))
            deckEditorPanel.gameObject.SetActive(true);
    }
}

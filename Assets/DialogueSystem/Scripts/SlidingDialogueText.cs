using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SlidingDialogueText : MonoBehaviour
{
    [SerializeField]
    private DialogueSequence _dialogueSequence;

    [SerializeField]
    private ScrollRect _scrollRect;

    private TextMeshProUGUI _slidingText;
    private StandardControls inputActions;
    private bool _isTyping;
    private bool _isSkipped;

    private void Start()
    {
        _slidingText = GetComponent<TextMeshProUGUI>();
        inputActions = new StandardControls();
        inputActions.Player.Interact.Enable();

        inputActions.Player.Interact.performed += (context) => { if (_isTyping) _isSkipped = true; };
    }

    private void Update()
    {
        foreach (var dialogue in _dialogueSequence.sequence)
        {
            if (!_isTyping && inputActions.Player.Interact.IsPressed())
            {
                StartCoroutine(TypeSymbols(dialogue));
            }
        }
    }

    private IEnumerator TypeSymbols(string dialogue)
    {
        _isTyping = true;

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (_isSkipped)
            {
                _slidingText.text = dialogue;
                _scrollRect.verticalNormalizedPosition = 0;
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
                _isSkipped = false;
                break;
            }
            _slidingText.text += dialogue[i];
            _scrollRect.verticalNormalizedPosition = 0;
            yield return new WaitForSeconds(_dialogueSequence.delayPerSymbol);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_scrollRect.transform);
        }

        _isTyping = false;
    }
}

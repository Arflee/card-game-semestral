using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBubble;

    private StandardControls inputActions;

    private void Start()
    {
        inputActions = new StandardControls();
        inputActions.Player.Interact.Enable();

        inputActions.Player.Interact.performed += OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        Debug.Log("clicked");
    }

    private void ShowDialogue()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;

    private bool _menuOn = false;
    private StandardControls _controls;

    private void Start()
    {
        _controls = new StandardControls();

        _controls.UI.Enable();
        _controls.UI.Cancel.performed += OnCancelButtonPress;
    }

    private void OnDisable()
    {
        _controls.UI.Cancel.performed -= OnCancelButtonPress;
    }

    private void OnCancelButtonPress(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        _menuOn = !_menuOn;
        menu.SetActive(_menuOn);
        settings.SetActive(false);
        Time.timeScale = _menuOn ? 0f : 1f;
    }
}

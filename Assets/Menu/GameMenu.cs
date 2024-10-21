using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;

    private bool menuOn;

    private void Start()
    {
        ShowMenu(menuOn);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowMenu(!menuOn);
        }
    }

    public void ShowMenu(bool show)
    {
        menuOn = show;
        menu.SetActive(show);
        settings.SetActive(false);
        Time.timeScale = show ? 0f : 1f;
    }
}

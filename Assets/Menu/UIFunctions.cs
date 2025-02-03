using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIFunctions : MonoBehaviour
{
    [System.Serializable]
    public class KeyEvent
    {
        public KeyCode key;
        public UnityEvent onKeyPress;
    }

    [SerializeField] private List<KeyEvent> keyEvents = new();

    private bool _menuOn = false;

    public void LoadScene(string sceneName)
    {
        LevelLoader.Instance.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowMenu(GameObject menu)
    {
        _menuOn = !_menuOn;
        menu.SetActive(_menuOn);
    }

    private void Update()
    {
        foreach (KeyEvent keyEvent in keyEvents)
        {
            if (Input.GetKeyDown(keyEvent.key))
            {
                keyEvent.onKeyPress?.Invoke();
            }
        }
    }
}

using System.Diagnostics;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Application.Quit());
    }
}

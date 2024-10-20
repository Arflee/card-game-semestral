using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string spawnPointName;

    private void OnEnable()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneLoader.Instance.LoadScene(sceneName, spawnPointName);
    }
}

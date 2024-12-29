using System;
using UnityEngine;

public class CrossScenePlayerState : MonoBehaviour
{
    public static CrossScenePlayerState Instance { get; private set; }

    public Vector3 Position { get; private set; }

    public string SceneName { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePosition(Vector3 position)
    {
        Position = position;
    }

    internal void SaveSceneName(string sceneName)
    {
        SceneName = sceneName;
    }
}

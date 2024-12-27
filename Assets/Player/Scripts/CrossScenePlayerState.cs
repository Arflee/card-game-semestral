using UnityEngine;

public class CrossScenePlayerState : MonoBehaviour
{
    public static CrossScenePlayerState Instance { get; private set; }

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void SavePosition(Vector3 position)
    {
        Position = position;
    }
}

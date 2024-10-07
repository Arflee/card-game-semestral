using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    public string sceneName;
    public string spawnPointName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out var player))
        {
            SceneLoader.Instance.LoadScene(sceneName, spawnPointName);
        }
    }
}

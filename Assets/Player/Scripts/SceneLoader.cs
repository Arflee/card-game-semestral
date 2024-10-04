using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    public static SceneLoader Instance
    { 
        get
        {
            if (instance == null)
            {
                GameObject go = new("Scene Loader");
                instance = go.AddComponent<SceneLoader>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public void LoadScene(string sceneName, string spawnPointName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, spawnPointName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string spawnPointName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return new WaitUntil(() => asyncLoad.isDone);

        GameObject spawnPoint = GameObject.Find(spawnPointName);
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.transform.position = spawnPoint.transform.position;
    }
}

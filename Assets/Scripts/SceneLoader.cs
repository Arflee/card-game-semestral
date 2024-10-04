using System.Collections;
using System.Collections.Generic;
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
                GameObject go = new GameObject("Scene Loader");
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = spawnPoint.transform.position;
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
    }
}

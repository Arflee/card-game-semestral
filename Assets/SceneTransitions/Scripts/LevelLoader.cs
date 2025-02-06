using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Serializable]
    struct SceneSaveElement
    {
        public string sceneId;
        public GameObject[] sceneObjects;
    }

    [SerializeField] private SceneSaveElement[] elementsToSave;
    [SerializeField] private Animator transitionAnimator;

    public event Action<AsyncOperation> OnLoadFinish;

    public static LevelLoader Instance { get; private set; }


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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadCoroutine(sceneName));
    }

    public void LoadSceneWithSpawnPoint(string sceneName, string spawnPointName)
    {
        StartCoroutine(LoadCoroutineWithSpawnPoint(sceneName, spawnPointName));
    }

    private IEnumerator LoadCoroutineWithSpawnPoint(string sceneName, string spawnPointName)
    {
        StateManager.Instance.SaveStates();
        yield return LoadCoroutine(sceneName);
        StateManager.Instance.LoadStates();

        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint == null)
            yield break;
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.transform.position = spawnPoint.transform.position;
    }

    private IEnumerator LoadCoroutine(string sceneName)
    {
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        var asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        asyncLoading.completed += OnLoadFinish;

        yield return new WaitUntil(() => asyncLoading.isDone);

        asyncLoading.completed -= OnLoadFinish;

        transitionAnimator.SetTrigger("End");
    }

}

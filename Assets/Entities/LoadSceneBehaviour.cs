using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneBehaviour : BehaviourState
{
    [SerializeField] private string sceneName;
    [SerializeField] private BehaviourState nextState; 

    protected override void OnEnable()
    {
        DOTween.Clear(true);
        var playerPosition = FindObjectOfType<PlayerMovement>().gameObject.transform.position;

        CrossScenePlayerState.Instance.SavePosition(playerPosition);
        CrossScenePlayerState.Instance.SaveSceneName(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}

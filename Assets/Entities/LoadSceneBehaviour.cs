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
        //CrossScenePlayerState.Instance.SavePosition()
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public override BehaviourState NextState()
    {
        return nextState;
    }
}

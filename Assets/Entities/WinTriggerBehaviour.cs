using UnityEngine;

public class WinTriggerBehaviour : BehaviourState
{
    [SerializeField] private string gameId;

    [SerializeField, Header("States")]
    private BehaviourState nextState;

    private void Start()
    {
        Finished();
    }

    public override BehaviourState NextState()
    {
        var endingHanlder = GameEndingHandler.Instance;
        var currentEnding = endingHanlder.GetGameEnding(gameId);
        return currentEnding switch
        {
            GameEndingHandler.Ending.Win => nextState,
            GameEndingHandler.Ending.Lose => null,
            GameEndingHandler.Ending.DidntFight => null,
            _ => null,
        };
    }
}

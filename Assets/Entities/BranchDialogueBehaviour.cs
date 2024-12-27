using UnityEngine;

public class BranchDialogueBehaviour : BehaviourState
{
    [SerializeField] private string gameId;
    [SerializeField] private DialogueTrigger dialogueOnDidntFight;
    [SerializeField] private DialogueTrigger dialogueOnWin;
    [SerializeField] private DialogueTrigger dialogueOnLose;

    private GameEndingHandler _gameEnding;

    [Header("States")]
    [SerializeField] private BehaviourState onWinNextState;
    [SerializeField] private BehaviourState onLoseNextState;
    [SerializeField] private BehaviourState onDidntFightNextState;

    protected override void OnEnable()
    {
        _gameEnding = GameEndingHandler.Instance;

        switch (_gameEnding.GetGameEnding(gameId))
        {
            case GameEndingHandler.Ending.Win:
                dialogueOnWin.OnDialogueEnd += Finished;
                dialogueOnWin.EnableDialogue();
                break;

            case GameEndingHandler.Ending.Lose:
                dialogueOnLose.OnDialogueEnd += Finished;
                dialogueOnLose.EnableDialogue();
                break;

            case GameEndingHandler.Ending.DidntFight:
                dialogueOnDidntFight.OnDialogueEnd += Finished;
                dialogueOnDidntFight.EnableDialogue();
                break;

            default:
                Debug.LogError("impossible game ending occured");
                break;
        }
    }

    public override BehaviourState NextState()
    {
        return _gameEnding.GetGameEnding(gameId) switch
        {
            GameEndingHandler.Ending.Win => onWinNextState,
            GameEndingHandler.Ending.Lose => onLoseNextState,
            GameEndingHandler.Ending.DidntFight => onDidntFightNextState,
            _ => onDidntFightNextState,
        };
    }
}

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
                if (dialogueOnWin != null)
                {
                    dialogueOnWin.OnDialogueEnd += Finished;
                    dialogueOnWin.EnableDialogue();
                }
                else if (onWinNextState != null)
                    onWinNextState.enabled = true;
                break;

            case GameEndingHandler.Ending.Lose:
                if (dialogueOnLose != null)
                {
                    dialogueOnLose.OnDialogueEnd += Finished;
                    dialogueOnLose.EnableDialogue();
                }
                else if (onLoseNextState != null)
                    onLoseNextState.enabled = true;
                break;

            case GameEndingHandler.Ending.DidntFight:
                if (dialogueOnDidntFight != null)
                {
                    dialogueOnDidntFight.OnDialogueEnd += Finished;
                    dialogueOnDidntFight.EnableDialogue();
                }
                else if (onDidntFightNextState != null)
                    onDidntFightNextState.enabled = true;
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

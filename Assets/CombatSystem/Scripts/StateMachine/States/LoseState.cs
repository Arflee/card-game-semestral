using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseState : CombatState
{
    private GameEndingHandler _gameHandler;

    public LoseState(CombatStateMachine machine) : base(machine)
    {
        _gameHandler = machine.GameHandler;
    }

    public override IEnumerator EnterState()
    {
        DOTween.Clear(true);
        _gameHandler.PlayerLostGame(SceneManager.GetActiveScene().name);
        
        yield return null;
    }

    public override CombatState NextState()
    {
        throw new System.NotImplementedException();
    }
}

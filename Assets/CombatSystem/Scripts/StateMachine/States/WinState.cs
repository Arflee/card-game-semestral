using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinState : CombatState
{
    private GameEndingHandler _gameHandler;

    public WinState(CombatStateMachine machine) : base(machine)
    {
        _gameHandler = machine.GameHandler;
    }

    public override IEnumerator EnterState()
    {

        if (StateMachine.EnemyInitializer.Enemy.Reward.Length > 0)
        {
            StateMachine.RewardPanel.gameObject.SetActive(true);
            StateMachine.RewardPanel.SetRewardCard(StateMachine.EnemyInitializer.Enemy.Reward);
        }

        StateMachine.RewardPanel.PanelButton.onClick.AddListener(() =>
        {
            _gameHandler.PlayerWonGame(SceneManager.GetActiveScene().name);
        });

        yield return null;
    }

    public override CombatState NextState()
    {
        return null;
    }
}

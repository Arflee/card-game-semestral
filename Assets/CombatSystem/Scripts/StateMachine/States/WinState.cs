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
        if (StateMachine.EnemyInitializer.GetRewardCards().Length > 0)
        {
            RewardPanel.Instance.gameObject.SetActive(true);
            RewardPanel.Instance.SetRewardCard(StateMachine.EnemyInitializer.GetRewardCards(), "Máte nové karty!");
        }

        RewardPanel.Instance.AddCallback(() =>
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

using UnityEngine;

public class GameHandler : MonoBehaviour
{
    enum GameEndings
    {
        Win,
        Lose
    }

    private GameEndings _lastPlayedGame;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayerLostGame()
    {
        _lastPlayedGame = GameEndings.Lose;
    }

    public void PlayerWonGame()
    {
        _lastPlayedGame = GameEndings.Win;
    }
}

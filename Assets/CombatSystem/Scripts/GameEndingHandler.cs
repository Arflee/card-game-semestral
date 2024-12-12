using UnityEngine;

public class GameEndingHandler : MonoBehaviour
{
    public enum Endings
    {
        Win,
        Lose
    }

    private Endings _lastPlayedGame;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayerLostGame()
    {
        _lastPlayedGame = Endings.Lose;
    }

    public void PlayerWonGame()
    {
        _lastPlayedGame = Endings.Win;
    }
}

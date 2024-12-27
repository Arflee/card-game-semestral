using System.Collections.Generic;
using UnityEngine;

public class GameEndingHandler : MonoBehaviour
{
    public enum Ending
    {
        Win,
        Lose,
        DidntFight
    }

    private Dictionary<string, Ending> _gameEndings = new();

    public Ending GetGameEnding(string gameId)
    {
        var gameWasPlayed = _gameEndings.TryGetValue(gameId, out var ending);
        if (!gameWasPlayed)
        {
            return Ending.DidntFight;
        }

        return ending;
    }

    public static GameEndingHandler Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void PlayerLostGame(string gameId)
    {
        _gameEndings[gameId] = Ending.Lose;
    }

    public void PlayerWonGame(string gameId)
    {
        _gameEndings[gameId] = Ending.Win;
    }
}

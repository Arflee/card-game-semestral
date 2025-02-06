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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PlayerLostGame(string gameId)
    {
        _gameEndings[gameId] = Ending.Lose;
        ReturnToLastScene();
    }

    public void PlayerWonGame(string gameId)
    {
        var playersDeck = FindObjectOfType<CardDeck>();
        var enemy = FindObjectOfType<EnemyInitializer>();

        var newCrystal = FindObjectOfType<AddCrystal>();

        if (newCrystal != null)
        {
            playersDeck.SetCrystals(newCrystal.NewCrystal);
        }

        foreach (var card in enemy.GetRewardCards())
        {
            playersDeck.AddNewCard(card);
        }

        _gameEndings[gameId] = Ending.Win;
        ReturnToLastScene();
    }

    private void ReturnToLastScene()
    {
        LevelLoader.Instance.OnLoadFinish += SceneLoadCompleted;
        LevelLoader.Instance.LoadScene(CrossScenePlayerState.Instance.SceneName);
    }

    private void SceneLoadCompleted(AsyncOperation obj)
    {
        var player = FindObjectOfType<PlayerMovement>();
        player.transform.position = CrossScenePlayerState.Instance.Position;
        LevelLoader.Instance.OnLoadFinish -= SceneLoadCompleted;
    }
}

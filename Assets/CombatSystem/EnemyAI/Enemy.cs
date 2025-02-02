using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private List<EnemyDeck> _enemyDeckPerCrystalsDestroyed;
    [SerializeField] private CombatCard[] _reward;
    [SerializeField] private LifeCrystalParameters _crystals;

    public List<EnemyDeck> EnemyDeckPerCrystalsDestroyed => _enemyDeckPerCrystalsDestroyed;
    public CombatCard[] Reward => _reward;
    public LifeCrystalParameters Crystals => _crystals;
}

[System.Serializable]
public class EnemyDeck : IEnumerable<CombatCard>
{
    [SerializeField] private List<CombatCard> _cards;
    [SerializeField] private bool _shuffleDeck = true;
    [SerializeField] private bool _reshufleWhenEmpty = true;
    [SerializeField] private int _cardsPerTurn = 1;

    public bool ShuffleDeck => _shuffleDeck;
    public bool ReshufleWhenEmpty => _reshufleWhenEmpty;
    public int CardsPerTurn => _cardsPerTurn;

    public IEnumerator<CombatCard> GetEnumerator()
    {
        return _cards.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _cards.GetEnumerator();
    }
}
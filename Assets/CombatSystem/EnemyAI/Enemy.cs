using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private CombatCard[] _enemyDeck;
    [SerializeField] private bool _shuffleDeck = true;
    [SerializeField] private bool _reshufleWhenEmpty = true;
    [SerializeField] private int _cardsPerTurn = 1;
    [SerializeField] private CombatCard[] _reward;
    [SerializeField] private LifeCrystalParameters _crystals;

    public CombatCard[] EnemyDeck => _enemyDeck;
    public bool ShuffleDeck => _shuffleDeck;
    public bool ReshufleWhenEmpty => _reshufleWhenEmpty;
    public int CardsPerTurn => _cardsPerTurn;
    public CombatCard[] Reward => _reward;
    public LifeCrystalParameters Crystals => _crystals;
}

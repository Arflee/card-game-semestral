using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [SerializeField] private List<EnemyDeck> _enemyDeckPerCrystalsDestroyed;
    [SerializeField] private CombatCard[] _reward;
    [SerializeField] private LifeCrystalParameters _crystals;
    [SerializeField] private DialogueSequence[] inGameDialogues;
    [SerializeField] private Tutorial tutorial;

    public List<EnemyDeck> EnemyDeckPerCrystalsDestroyed => _enemyDeckPerCrystalsDestroyed;
    public CombatCard[] Reward => _reward;
    public LifeCrystalParameters Crystals => _crystals;
    
    public CombatState StartingCombatState(CombatStateMachine machine)
    {
            Queue<DialogueSequence> dialogues;
        if (inGameDialogues == null)
            dialogues = null;
        else
            dialogues = new Queue<DialogueSequence>(inGameDialogues);

        switch (tutorial)
        {
            case Tutorial.TotalIntro:
                return new Tutorial1BegginingState(machine, dialogues);
            case Tutorial.AdvancedTechniques:
                return new Tutorial2BegginingState(machine, dialogues);
            case Tutorial.Crystals:
                return new Tutorial3BegginingState(machine, dialogues);
            default:
                return new EnemyState(machine);
        }
    }
}

public enum Tutorial { None, TotalIntro, AdvancedTechniques, Crystals };

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
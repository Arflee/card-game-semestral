using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyHand;

    private CombatSlot[] _slots;

    private void Start()
    {
        var slotsGO = GameObject.FindGameObjectsWithTag("EnemySlot");
        _slots = slotsGO.Select((el) => { return el.GetComponent<CombatSlot>(); }).ToArray();

        for (int i = 0; i < _slots.Length; i++)
        {
            CardVisual visual = Instantiate(visualPrefab, _slots[i].transform);
            CombatCard currentCard = enemyHand[i % _slots.Length];

            _slots[i].PutCardInSlot(currentCard);
            visual.Initialize(currentCard);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyHand;

    private List<CombatCardDTO> _cardObjects;
    private CombatSlot[] _slots;

    private void Start()
    {
        _cardObjects = new();

        foreach (var item in enemyHand)
        {
            _cardObjects.Add(new CombatCardDTO(item));
        }

        var slotsGO = GameObject.FindGameObjectsWithTag("EnemySlot");
        _slots = slotsGO.Select((el) => { return el.GetComponent<CombatSlot>(); }).ToArray();

        for (int i = 0; i < _slots.Length; i++)
        {
            CardVisual visual = Instantiate(visualPrefab, _slots[i].transform);
            CombatCardDTO currentCard = _cardObjects[i % _slots.Length];

            _slots[i].PutCardInSlot(currentCard);
            visual.Initialize(currentCard);
        }
    }
}

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField] private CardVisual visualPrefab;
    [SerializeField] private CombatCard[] enemyHand;

    private List<CombatCardDTO> _cardObjects;
    private CombatSlot[] _slots;

    public IEnumerator Initialize()
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
            yield return new WaitForSeconds(0.5f);
            CombatCardDTO currentCard = _cardObjects[i % _slots.Length];

            _slots[i].PutCardInSlot(currentCard);
            visual.Initialize(currentCard);
        }
    }
}

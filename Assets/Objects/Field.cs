using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = Vector2Int.one;
    [SerializeField] private Vector2 cellSize = Vector2.one;
    [SerializeField] private EatabeWheat prefab;

    public float WheatDestroyed => 1 - (Wheat.Count / (float)(gridSize.x * gridSize.y));

    public List<EatabeWheat> Wheat { get; private set; } = new List<EatabeWheat>();

    void Awake()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var wheat = Instantiate(prefab, CellCenter(x, y), Quaternion.identity);
                wheat.field = this;
                Wheat.Add(wheat);
            }
        }
    }

    private Vector2 CellCenter(int x, int y)
    {
        Vector3 offset = -(gridSize - Vector2.one) / 2f * cellSize;
        return transform.position + offset + (Vector3)(new Vector2(x, y) * cellSize);
    }

    public void DestroyWheat(EatabeWheat wheat)
    {
        Wheat.Remove(wheat);
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Gizmos.DrawWireCube(CellCenter(x, y), cellSize);
            }
        }
    }
}

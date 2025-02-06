using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrystal : MonoBehaviour
{
    [SerializeField] private AddCrystal prefab;
    
    public void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }
}

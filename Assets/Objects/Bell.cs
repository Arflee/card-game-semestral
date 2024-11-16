using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject soldier;

    public void Ring()
    {
        source.Play();
        Instantiate(soldier);
    }
}

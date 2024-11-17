using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject helper;
    [SerializeField] private LayerMask bearLayer;

    public void Ring()
    {
        source.Play();
        Instantiate(soldier);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && helper.activeInHierarchy)
            Ring();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            helper.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            helper.SetActive(false);
        }
    }
}

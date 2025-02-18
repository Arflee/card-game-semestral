using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private GameObject soldier;
    [SerializeField] private LayerMask bearLayer;

    bool close = false;

    public void Ring()
    {
        source.Play();
        Instantiate(soldier);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && close)
            Ring();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            close = true;
            InteractText.Instance.Use(this, "zazvo�");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            close = false;
            InteractText.Instance.Disable(this);
        }
    }
}

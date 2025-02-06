using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCredits : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out var player))
        {
            Destroy(FindObjectOfType<CardDeck>().gameObject);
            LevelLoader.Instance.LoadScene(sceneName);
        }
    }
}

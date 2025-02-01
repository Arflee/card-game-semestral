using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardCharsSO : ScriptableObject
{
    [Header("Card parameters")]
    [SerializeField] private int manaCost;

    [Header("Card information")]
    [SerializeField] private string cardName;
    [SerializeField, TextArea(3, 7)] private string cardDescription;
    [SerializeField] private Sprite cardSprite;

    public Sprite CardSprite => cardSprite;
    public string Name => cardName;
    public string Description => cardDescription;

    public int ManaCost => manaCost;
}

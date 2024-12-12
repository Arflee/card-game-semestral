using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    //https://www.wikiwand.com/en/articles/Fisher-Yates_shuffle
    public static List<T> Shuffle<T>(IEnumerable<T> listToShuffle)
    {
        List<T> copiedDeck = new(listToShuffle);

        List<T> shuffled = new();
        System.Random random = new();

        while (copiedDeck.Count > 0)
        {
            int k = random.Next(copiedDeck.Count);
            shuffled.Add(copiedDeck[k]);
            copiedDeck.RemoveAt(k);
        }

        return shuffled;
    }
}

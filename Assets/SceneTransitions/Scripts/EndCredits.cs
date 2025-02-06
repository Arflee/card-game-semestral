using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public void BackToMenu()
    {
        LevelLoader.Instance.LoadScene("MainMenu");
    }
}

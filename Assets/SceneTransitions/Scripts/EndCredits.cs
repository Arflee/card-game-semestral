using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public void BackToMenu()
    {
        LevelLoader.Instance.LoadScene("MainMenu");
    }
}

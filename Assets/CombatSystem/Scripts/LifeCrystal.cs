using TMPro;
using UnityEngine;

public class LifeCrystal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    public int Health { get; private set; }

    public void Initialize(int health)
    {
        healthText.text = health.ToString();
        Health = health;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthText.text = Health.ToString();
    }
}

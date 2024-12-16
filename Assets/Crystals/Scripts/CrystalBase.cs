using TMPro;
using UnityEngine;

public class CrystalBase : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int crystalAmount = 3;
    [SerializeField] private TextMeshProUGUI healthText;

    public int Health { get; private set; }
    public int CrystalAmount => crystalAmount;

    public void Start()
    {
        Health = health;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        healthText.text = Health.ToString();
    }
}

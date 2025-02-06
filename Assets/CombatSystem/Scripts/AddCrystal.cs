using UnityEngine;

public class AddCrystal : MonoBehaviour
{
    private void Eat()
    {
        var reward = RewardPanel.Instance;
        reward.gameObject.SetActive(true);
        reward.SetRewardCrystal("Máte další krystal!");
        reward.AddCallback(() => reward.ClearBoard());
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Eat();
    }
}

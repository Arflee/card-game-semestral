using DG.Tweening;
using TMPro;
using UnityEngine;

public class ManaPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private float shakeStrength = 5;

    private int _maxAmount, _currentAmount;

    public void UseManaCrystals(int amount)
    {
        _currentAmount -= amount;
        UpdateText();
    }

    public void ResetManaCrystals(int newCrystalAmount)
    {
        _maxAmount = newCrystalAmount;
        _currentAmount = newCrystalAmount;
        UpdateText();
    }

    private void UpdateText()
    {
        manaText.text = $"{_currentAmount}/{_maxAmount}";
    }

    public void NotEnoughManaAnimation()
    {
        transform.DOShakePosition(1f, strength: shakeStrength);
    }
}

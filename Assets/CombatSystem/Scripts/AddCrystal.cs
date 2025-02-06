using UnityEngine;

public class AddCrystal : MonoBehaviour
{
    [SerializeField] private LifeCrystalParameters newCrystal;

    public LifeCrystalParameters NewCrystal => newCrystal;
}

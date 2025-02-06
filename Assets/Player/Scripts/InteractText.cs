using TMPro;
using UnityEngine;

public class InteractText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    public static InteractText Instance { get; private set; }

    private Object _caller;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Use(Object caller, string text)
    {
        _caller = caller;
        tmp.text = "[e] " + text;
        gameObject.SetActive(true);
    }

    public void Disable(Object caller)
    {
        if (_caller == caller)
            gameObject.SetActive(false);
    }
}

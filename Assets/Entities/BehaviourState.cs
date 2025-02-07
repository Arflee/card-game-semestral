using System;
using UnityEngine;

public abstract class BehaviourState : MonoBehaviour
{
    public bool saveState = true;
    private string _id;

    public event Func<bool> CancelFinish;

    public abstract BehaviourState NextState();

    protected virtual void Awake()
    {
        // Generate unique id
        // Breaks if multiple objects have the same name and behaviour type
        string name = gameObject.name;
        string script = GetType().Name;
        string index = "-1";
        Component[] components = GetComponents(GetType());
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i] == this)
            {
                index = i.ToString();
                break;
            }
        }
        _id = $"{name}_{script}_{index}";
    }

    public string GetId()
    {
        return _id;
    }

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

    public void Finished()
    {
        if (CancelFinish?.Invoke() ?? false)
        {
            return;
        }

        enabled = false;
        BehaviourState next = NextState();
        if (next != null)
        {
            next.enabled = true;
        }
    }
}

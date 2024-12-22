using System;
using UnityEngine;

public abstract class BehaviourState : MonoBehaviour
{
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
        Debug.Log("ID: " + _id, this);
    }

    protected virtual void OnEnable()
    {
        Debug.Log("Enabled: " + ToString(), this);

        // Load state if it was previously saved
        if (StateManager.Instance.HasState(_id))
        {
            enabled = StateManager.Instance.GetState(_id);
        }
    }

    protected virtual void OnDisable()
    {
        // Save state
        StateManager.Instance.SetState(_id, enabled);
    }

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

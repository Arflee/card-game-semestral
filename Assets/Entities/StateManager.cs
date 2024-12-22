using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public Dictionary<string, bool> _states = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(string objectId, bool state)
    {
        _states[objectId] = state;
    }

    public bool GetState(string objectId)
    {
        return _states[objectId];
    }

    public bool HasState(string objectId)
    {
        return _states.ContainsKey(objectId);
    }
}

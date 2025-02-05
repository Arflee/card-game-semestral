using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    public Dictionary<string, bool> _states = new Dictionary<string, bool>();

    private void Awake()
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

    public void SaveStates()
    {
        BehaviourState[] states = FindObjectsOfType<BehaviourState>();
        foreach (BehaviourState state in states)
        {
            _states[state.GetId()] = state.enabled;
        }
    }

    public void LoadStates()
    {
        BehaviourState[] states = FindObjectsOfType<BehaviourState>();
        foreach (BehaviourState state in states)
        {
            if (_states.ContainsKey(state.GetId()))
            {
                state.enabled = _states[state.GetId()];
            }
        }
    }
}

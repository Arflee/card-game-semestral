using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipFight : MonoBehaviour
{
    public CombatStateMachine manager;

    public void Awake()
    {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }

    public void Skip()
    {
#if UNITY_EDITOR
        if (manager == null)
            manager = FindObjectOfType<CombatStateMachine>();

        manager.gameObject.SetActive(true);
        manager.SetState(new WinState(manager));
#endif
    }
}

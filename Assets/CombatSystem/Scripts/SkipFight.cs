using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipFight : MonoBehaviour
{
    public CombatStateMachine manager;

    public void Skip()
    {
#if UNITY_EDITOR
        if (manager == null)
            manager = FindObjectOfType<CombatStateMachine>();

        manager.SetState(new WinState(manager));
#endif
    }
}

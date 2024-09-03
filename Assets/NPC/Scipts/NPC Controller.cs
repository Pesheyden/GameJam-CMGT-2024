using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool IsPossessed;
    private NPCMovement _npcMovement;
    private NPCStats _npcStats;

    private void Awake()
    {
        _npcMovement = GetComponent<NPCMovement>();
        _npcStats = GetComponent<NPCStats>();
    }

    public (NPCStatsBlock, Transform)  Possess()
    {
        _npcMovement.StopMovement();
        gameObject.SetActive(false);
        return (_npcStats.NpcStatsBlock, transform);
    }

    public void UnPosses()
    {
        gameObject.SetActive(true);
        _npcMovement.StartMovement();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
    public NPCStatsBlock NpcStatsBlock;
}
[Serializable]
public class NPCStatsBlock
{
    [Header("PossessionStats")]
    public Sprite CharacterSprite;
    [Tooltip("0 - no keys")]public int KeyLevel;
    public float Speed;
    public float PossessionTime;
}

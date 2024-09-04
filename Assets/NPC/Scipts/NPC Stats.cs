using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NPCStats : MonoBehaviour
{
    public NPCStatsBlock NpcStatsBlock;
}
[Serializable]
public class NPCStatsBlock
{
    [Header("PossessionStats")]
    public GameObject CharacterPrefab;
    public Vector2 ColliderSize;
    [Tooltip("0 - no keys")]public int KeyLevel;
    public float Speed;
    public float JumpPower;
    public float PossessionTime;
}

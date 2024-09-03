using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public PlayerGhostShoot PlayerGhostShoot;
    [SerializeField] private float _existTime;

    private void Awake()
    {
        StartCoroutine(Timer());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("NPC"))
        {
            PlayerController.Instance.StartPossession(other.gameObject.GetComponent<NPCController>());
        }
        else
        {
            PlayerGhostShoot.TurnOnShootingMode();
        }
        Destroy(gameObject);
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_existTime);
        Destroy(gameObject);
        PlayerGhostShoot.TurnOnShootingMode();
    }
}

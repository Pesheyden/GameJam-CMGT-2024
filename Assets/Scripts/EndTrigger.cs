using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(1);
        if(other.gameObject.CompareTag("Player"))
            GameManager.Instance.GameWin();
    }
}

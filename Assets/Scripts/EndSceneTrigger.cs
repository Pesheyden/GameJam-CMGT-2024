using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _anim;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Ghost")
        {
            Camera.main.gameObject.SetActive(false);
            PlayerController.Instance.ChangePlayerStatus(false);
            _anim.SetActive(true);
            _anim.GetComponent<Animation>().Play();
            Invoke("OpenMenu", 60f);
        }
    }

    private void OpenMenu()
    {
        GameManager.Instance.SetScene("MainMenu");
    }
}

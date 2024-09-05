using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int keyRequirement;

    private float previous;
    public SoundPlayer sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        // if (collider.gameObject.CompareTag("NPC"))
        // {
        //     if(collider.gameObject.GetComponent<NPCStats>().NpcStatsBlock.KeyLevel == keyRequirement)
        //     {
        //         Open();
        //         sound.StartPlayingProcess();
        //         Invoke("Close", 2);
        //     }
        // }
        if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.GetComponent<PlayerController>().KeyLevel == keyRequirement)
            {
                Open();
                sound.StartPlayingProcess();
                Invoke("Close", 10);
            }
        }

    }


    private void Open()
    {
        previous = transform.position.y;
        transform.position = new Vector2(transform.position.x, -2000);
    }
    private void Close()
    {
        transform.position = new Vector2(transform.position.x, previous);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int keyRequirement;

    private float previous;

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
        Debug.Log(1);
        if (collider.gameObject.CompareTag("NPC"))
        {
            if(collider.gameObject.GetComponent<NPCStats>().NpcStatsBlock.KeyLevel == keyRequirement)
            {
                Open();
                Invoke("Close", 2);
            }
        }else if (collider.gameObject.CompareTag("Player"))
        {
            if (collider.gameObject.GetComponent<PlayerController>().KeyLevel == keyRequirement)
            {
                Open();
                Invoke("Close", 2);
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

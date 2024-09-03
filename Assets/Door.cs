using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int keyRequirement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(1);
        if (other.gameObject.CompareTag("NPC"))
        {
            if(other.gameObject.GetComponent<NPCStats>().NpcStatsBlock.KeyLevel == keyRequirement)
            {
                Open();
                Invoke("Close", 2);
            }
        }

    }


    private void Open()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    private void Close()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}

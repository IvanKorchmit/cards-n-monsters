using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    public Transform exit;



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = exit.position + new Vector3(Random.Range(-1,1),Random.Range(-1,1));
        }
    }
}

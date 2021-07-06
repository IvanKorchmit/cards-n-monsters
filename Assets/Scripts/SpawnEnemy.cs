using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    private GameObject[] count;
    public int SpawnLT;
    public float Cooldown;
    private float Timer;
    private void FixedUpdate()
    {
        Timer += Time.deltaTime;
        if(Timer >= Cooldown)
        {
            Timer = 0;
            if(!isEnoughEnemy())
            {
                var en = Instantiate(enemy, transform.position, Quaternion.identity);
                en.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            }
        }
    }


    public bool isEnoughEnemy()
    {
        count = GameObject.FindGameObjectsWithTag("Enemy");
        int i = 0;
        foreach (var e in count)
        {
            if (e == null) continue;
            if(e.name.Contains(enemy.name))
            {
                i++;
            }
        }

        return i > SpawnLT;
    }
}

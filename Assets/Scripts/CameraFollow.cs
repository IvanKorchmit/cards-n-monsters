using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void FixedUpdate()
    {
        Vector3 newLerped = Vector3.Lerp(transform.position, player.transform.position, 0.1F);
        newLerped.z = -10;
        transform.position = newLerped;
    }
}
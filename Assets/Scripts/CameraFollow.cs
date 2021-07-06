using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<AudioSource>().volume = Settings.musicVolume;
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 newLerped = Vector3.Lerp(transform.position, player.transform.position, 0.1F);
            newLerped.z = -10;
            transform.position = newLerped;
        }
    }
}

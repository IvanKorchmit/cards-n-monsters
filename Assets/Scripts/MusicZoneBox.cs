using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicZoneBox : MonoBehaviour
{
    public AudioClip clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name.Contains("Player"))
        {
            if (Camera.main.GetComponent<AudioSource>().clip != clip)
            {
                MusicZoneTheme.newMusic = clip;
                Camera.main.GetComponent<Animator>().SetTrigger("Change");
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.name.Contains("Player"))
        {
            if (Camera.main.GetComponent<AudioSource>().clip != clip)
            {
                MusicZoneTheme.newMusic = clip;
                Camera.main.GetComponent<Animator>().SetTrigger("Change");
            }
        }
    }
}

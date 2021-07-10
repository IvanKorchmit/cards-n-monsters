using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsStatic : MonoBehaviour
{
    public AudioClip hit;
    public AudioClip pickup;
    public AudioClip explosion;
    public AudioClip drink;
    public AudioClip craft;

    public static AudioClip Hit;
    public static AudioClip PickUp;
    public static AudioClip Explosion;
    public static AudioClip Craft;
    public static AudioClip Drink;
    private void Start()
    {
        Hit = hit;
        PickUp = pickup;
        Explosion = explosion;
        Craft = craft;
        Drink = drink;
    }
    public static void PlayPitched(AudioClip clip)
    {
        AudioSource audio = new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.pitch = Random.Range(0.7f, 1.4f);
        audio.volume = 1.0f * Settings.SFX;
        audio.Play();
        audio.transform.position = Camera.main.transform.position;
        Destroy(audio.gameObject, clip.length);
    }
    public static void Play(AudioClip clip)
    {
        AudioSource audio = new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = 1.0f * Settings.SFX;
        audio.Play();
        audio.transform.position = Camera.main.transform.position;
        Destroy(audio.gameObject, clip.length);
    }
}

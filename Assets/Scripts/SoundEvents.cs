using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvents : MonoBehaviour
{
    public void PlayPitched(AudioClip clip)
    {
        AudioSource audio = new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.pitch = Random.Range(0.7f, 1.4f);
        audio.volume = 1.0f * Settings.SFX;
        audio.Play();
        audio.transform.position = Camera.main.transform.position;
        Destroy(audio.gameObject, clip.length);
    }
    public void Play(AudioClip clip)
    {
        AudioSource audio = new GameObject("Audio", typeof(AudioSource)).GetComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = 1.0f * Settings.SFX;
        audio.Play();
        audio.transform.position = Camera.main.transform.position;
        Destroy(audio.gameObject, clip.length);
    }
}
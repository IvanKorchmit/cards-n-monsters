using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsStatic : MonoBehaviour
{
    public AudioClip hit;
    public AudioClip pickup;

    public static AudioClip Hit;
    public static AudioClip PickUp;
    private void Start()
    {
        Hit = hit;
        PickUp = pickup;
    }
}

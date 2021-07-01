using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrailEvent : MonoBehaviour
{
    public void ClearTrail()
    {
        transform.Find("Trail").GetComponent<TrailRenderer>().Clear();
    }
}

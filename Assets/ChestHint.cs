using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHint : MonoBehaviour
{
    private Transform hint;
    private void Start()
    {
        hint = transform.Find("HintHit");
        hint.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hint.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hint.gameObject.SetActive(false);
        }
    }
}

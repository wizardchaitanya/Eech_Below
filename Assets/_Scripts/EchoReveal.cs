using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoReveal : MonoBehaviour
{
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1,1,1,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Echowave"))
        {
            Debug.Log("Revealed");
            StopAllCoroutines();
            StartCoroutine(Reveal());
        }
    }

    IEnumerator Reveal()
    {
        sr.color = new Color(1,1,1,1);
        yield return new WaitForSeconds(5f);
        sr.color = new Color(1,1,1,0);
    }
}

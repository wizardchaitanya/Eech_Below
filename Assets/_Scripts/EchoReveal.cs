using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoReveal : MonoBehaviour
{
    SpriteRenderer sr;

    public float fadeSpeed = 3f;
    public float visibleTime = 5f;
    public bool canKill = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1,1,1,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Echowave"))
        {
            StopAllCoroutines();
            StartCoroutine(Reveal());
        }

        if (canKill && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDeath>()?.Die("Echo Kill");
        }
    }

    IEnumerator Reveal()
    {
        // FADE IN
        while (sr.color.a < 1f)
        {
            float a = Mathf.MoveTowards(sr.color.a, 1f, fadeSpeed * Time.deltaTime);
            sr.color = new Color(1, 1, 1, a);
            yield return null;
        }

        // STAY VISIBLE
        yield return new WaitForSeconds(visibleTime);

        // FADE OUT
        while (sr.color.a > 0f)
        {
            float a = Mathf.MoveTowards(sr.color.a, 0f, fadeSpeed * Time.deltaTime);
            sr.color = new Color(1, 1, 1, a);
            yield return null;
        }
    }
}

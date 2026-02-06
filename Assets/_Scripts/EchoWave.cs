using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoWave : MonoBehaviour
{
    public float speed = 10f;
    public float maxSize = 15f;

    float size;

    void Update()
    {
        size += speed * Time.deltaTime;

        transform.localScale = Vector3.one * size;

        if (size >= maxSize)
            Destroy(gameObject);
    }
}

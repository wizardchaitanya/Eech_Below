using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EchoWave : MonoBehaviour
{
    public float speed = 10f;
    public float maxSize = 15f;

    EchoWave wave;
    EchoTileReveal tileReveal;
    Tilemap tilemap;

    float size;

    void Start()
    {
        wave = GetComponent<EchoWave>();
        tileReveal = FindObjectOfType<EchoTileReveal>();
        tilemap = tileReveal.GetComponent<Tilemap>();
    }

    void Update()
    {
        size += speed * Time.deltaTime;

        transform.localScale = Vector3.one * size;

        RevelTiles();

        if (size >= maxSize)
            Destroy(gameObject);
    }

    void RevelTiles()
    {
        float radius = transform.localScale.x / 2f;

        Vector3Int centerCell = tilemap.WorldToCell(transform.position);
        int cellRadius = Mathf.CeilToInt(radius / tilemap.cellSize.x);

        for (int x = -cellRadius; x <= cellRadius; x++)
        {
            for (int y = -cellRadius; y <= cellRadius; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);
                Vector3 worldPos = tilemap.GetCellCenterWorld(cell);

                if (Vector2.Distance(worldPos, transform.position) <= radius)
                {
                    if (tilemap.HasTile(cell))
                    {
                        tileReveal.RevealTile(cell);
                    }
                }
            }
        }
    }
}

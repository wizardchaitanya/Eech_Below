using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    void OnTriggerEnter2D(Collider2D other)
    {
        EchoEnemyAI enemy = other.GetComponent<EchoEnemyAI>();
        if (enemy != null)
        {
            enemy.HearEcho(transform.position);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        EchoTileReveal reveal = other.GetComponentInParent<EchoTileReveal>();
        if (reveal == null) return;

        Grid grid = reveal.GetComponent<Grid>();
        if (grid == null) return;

        CircleCollider2D circle = GetComponent<CircleCollider2D>();
        float radius = circle.radius * transform.localScale.x;

        Vector3 centerWorld = transform.position;
        Vector3Int centerCell = grid.WorldToCell(centerWorld);

        int cellRadius = Mathf.CeilToInt(radius / grid.cellSize.x);

        for (int x = -cellRadius; x <= cellRadius; x++)
        {
            for (int y = -cellRadius; y <= cellRadius; y++)
            {
                Vector3Int cell = centerCell + new Vector3Int(x, y, 0);
                Vector3 cellWorld = grid.GetCellCenterWorld(cell);

                if (Vector2.Distance(cellWorld, centerWorld) <= radius)
                {
                    reveal.RevealTile(cell);
                }
            }
        }
    }
}

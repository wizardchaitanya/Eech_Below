using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class EchoTileReveal : MonoBehaviour
{
    Tilemap tilemap;

    Dictionary<Vector3Int, float> revealedTiles = new Dictionary<Vector3Int, float>();
    List<Vector3Int> pendingReveal = new List<Vector3Int>();

    public float revealDuration = 1.2f;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        // Make every tile invisible at start
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tilemap.SetTileFlags(pos, TileFlags.None);
                tilemap.SetColor(pos, new Color(1, 1, 1, 0));
            }
        }
    }

    void Update()
    {
        // APPLY queued reveals
        if (pendingReveal.Count > 0)
        {
            foreach (var cell in pendingReveal)
            {
                SetTileAlpha(cell, 1f);
                revealedTiles[cell] = revealDuration;
            }
            pendingReveal.Clear();
        }

        // 🔒 SAFE ITERATION: COPY KEYS
        var keys = new List<Vector3Int>(revealedTiles.Keys);

        foreach (var cell in keys)
        {
            revealedTiles[cell] -= Time.deltaTime;

            float alpha = Mathf.Clamp01(revealedTiles[cell] / revealDuration);
            SetTileAlpha(cell, alpha);

            if (revealedTiles[cell] <= 0f)
            {
                SetTileAlpha(cell, 0f);
                revealedTiles.Remove(cell);
            }
        }
    }

    void SetTileAlpha(Vector3Int cellPos, float alpha)
    {
        tilemap.SetTileFlags(cellPos, TileFlags.None);
        tilemap.SetColor(cellPos, new Color(1, 1, 1, alpha));
    }

    // 🚨 IMPORTANT: this no longer touches the dictionary directly
    public void RevealTile(Vector3Int cellPos)
    {
        if (!pendingReveal.Contains(cellPos))
            pendingReveal.Add(cellPos);
    }
}

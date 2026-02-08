using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMovement respawn = other.GetComponent<PlayerMovement>();
        if (respawn != null)
        {
            respawn.SetSpawnPoint(spawnPoint);
        }
    }
}

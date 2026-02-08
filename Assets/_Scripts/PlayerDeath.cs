using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    //bool isDead = false;

    public void Die(string reason)
    {
        //if (isDead) return;
        //isDead = true;

        Debug.Log("Player died: " + reason);

        // Freeze input
        //GetComponent<PlayerMovement>().enabled = false;

        // Play death sound / animation here

        // Restart room or reload checkpoint
        GetComponent<PlayerMovement>()?.Respawn();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}

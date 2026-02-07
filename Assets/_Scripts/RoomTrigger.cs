using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Room currentRoom;
    public Room nextRoom;

    CameraController cam;
    bool playerInside = false;
    bool transitionStarted = false;

    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>().GetComponent<CameraController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (transitionStarted) return;

        transitionStarted = true;
        playerInside = true;

        // Activate next room
        nextRoom.ActivateRoom();

        // Move camera
        cam.MoveToRoom(nextRoom);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = false;
        TryDeactivateRoom();
    }

    void Update()
    {
        TryDeactivateRoom();
    }

    void TryDeactivateRoom()
    {
        // Only deactivate when:
        // 1. Player left trigger
        // 2. Camera finished moving
        if (!playerInside && transitionStarted && !cam.IsTransitioning)
        {
            currentRoom.DeactivateRoom();
            Destroy(gameObject); // prevent re-trigger
        }
    }
}

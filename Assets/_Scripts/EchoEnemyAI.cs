using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRadius = 1.5f;
    public string deathReason = "Killed";


    Transform target;
    Vector3 soundPosition;

    enum State { Idle, Investigating, Chasing }
    State currentState = State.Idle;

    void Update()
    {
        switch (currentState)
        {
            case State.Investigating:
                MoveToSound();
                break;

            case State.Chasing:
                ChasePlayer();
                break;
        }
    }

    // 🔊 CALLED BY ECHO WAVE
    public void HearEcho(Vector3 echoPos)
    {
        soundPosition = echoPos;
        currentState = State.Investigating;
    }

    void MoveToSound()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            soundPosition,
            moveSpeed * Time.deltaTime
        );

        // If reached sound location
        if (Vector2.Distance(transform.position, soundPosition) < 0.1f)
        {
            currentState = State.Idle;
        }
    }

    void ChasePlayer()
    {
        if (target == null)
        {
            currentState = State.Idle;
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            chaseSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
            currentState = State.Chasing;
            other.GetComponent<PlayerDeath>()?.Die(deathReason);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            currentState = State.Idle;
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

public class EscapeTimer : MonoBehaviour
{
    public float timeLimit = 180f; // 3 minutes
    float timeRemaining;

    public UnityEvent OnTimeWarning;
    public UnityEvent OnTimeExpired;

    bool isRunning = false;
    public bool IsRunning => isRunning;

    void Start()
    {
        timeRemaining = timeLimit;
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 30f)
        {
            OnTimeWarning?.Invoke();
        }

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            OnTimeExpired?.Invoke();
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}

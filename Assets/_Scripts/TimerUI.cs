using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public EscapeTimer escapeTimer;
    public TextMeshProUGUI text;

    public Color normalColor = Color.white;
    public Color warningColor = Color.red;

    void Update()
    {
        if (escapeTimer == null) return;

        if (!escapeTimer.IsRunning)
        {
            text.enabled = false;
            return;
        }
        else
        {
            text.enabled = true;
        }

        float time = escapeTimer.GetTimeRemaining();

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        text.text = $"{minutes:00}:{seconds:00}";

        // Color warning
        if (time <= 30f)
            text.color = warningColor;
        else
            text.color = normalColor;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerTimer : MonoBehaviour
{
    public Text timerText; // UI Text to display the timer
    private float startTime;
    private bool timerRunning;

    void Start()
    {
        startTime = Time.time;
        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}

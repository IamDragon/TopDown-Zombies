using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float timerLength;
    private float timeLeft;
    private bool autoReset;


    /// <summary>
    /// Creates a new timer
    /// </summary>
    /// <param name="timerLength">The length of the timer in seconds</param>
    /// <param name="autoReset">Flag to reset the timer automatically when finished</param>
    public Timer(float timerLength, bool autoReset)
    {
        this.timerLength = timerLength;
        this.autoReset = autoReset;

        timeLeft = 0;
    }

    /// <summary>
    /// Increases the timer by a specified amount
    /// </summary>
    /// <param name="deltaTime">Timechange since last tick</param>
    /// <returns>True if timer is finished</returns>
    public bool TickTimer(float deltaTime)
    {
        timeLeft += deltaTime;

        if (timeLeft >= timerLength) //finnished
        {
            if (autoReset)
                ResetTimer();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Resets the timer
    /// </summary>
    public void ResetTimer()
    {
        timeLeft = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monitors the speedup effect
/// </summary>
public class SpeedupEffectMonitor : MonoBehaviour
{
    // speedup effect support
    Timer speedUpEffectTimer;
    float speedUpFactor;

    /// <summary>
    /// Gets whether or not the speedup effect is active
    /// </summary>
    /// <value><c>true</c> if speedup effect active; otherwise, <c>false</c>.</value>
    public bool SpeedupEffectActive
    {
        get { return speedUpEffectTimer.Running; }
    }

    /// <summary>
    /// Gets how many seconds are left in the speedup effect
    /// </summary>
    /// <value>speedup effect seconds left</value>
    public float SpeedupEffectSecondsLeft
    {
        get { return speedUpEffectTimer.SecondsLeft; }
    }

    /// <summary>
    /// Gets the speedup factor for the speedup effect
    /// </summary>
    /// <value>speedup factor</value>
    public float SpeedUpFactor
    {
        get { return speedUpFactor; }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        speedUpEffectTimer = gameObject.AddComponent<Timer>();
        speedUpEffectTimer.AddTimerFinishedListener(HandleSpeedUpEffectTimerFinished);
        EventManager.AddSpeedUpEffectListener(HandleSpeedupEffectActivatedEvent);
    }


    /// <summary>
    /// Handles the speedup effect activated event
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup factor</param>
    void HandleSpeedupEffectActivatedEvent(float duration, float speedupFactor)
    {
        // run or add time to timer
        if (!speedUpEffectTimer.Running)
        {
            this.speedUpFactor = speedupFactor;
            speedUpEffectTimer.Duration = duration;
            speedUpEffectTimer.Run();
        }
        else
        {
            speedUpEffectTimer.AddTime(duration);
        }
    }

    private void HandleSpeedUpEffectTimerFinished()
    {
        speedUpEffectTimer.Stop();
        speedUpFactor = 1;
    }
}

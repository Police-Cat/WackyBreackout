using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils 
{
    public static bool SpeedupEffectActive
    {
        get { return GetSpeedupEffectMonitor().SpeedupEffectActive; }
    }

    /// <summary>
    /// Gets how many seconds are left in the speedup effect
    /// </summary>
    /// <value>speedup effect seconds left</value>
    public static float SpeedupEffectSecondsLeft
    {
        get { return GetSpeedupEffectMonitor().SpeedupEffectSecondsLeft; }
    }

    /// <summary>
    /// Gets the speedup factor for the speedup effect
    /// </summary>
    /// <value>speedup factor</value>
    public static float SpeedUpScale
    {
        get { return GetSpeedupEffectMonitor().SpeedUpFactor; }
    }


    private static SpeedupEffectMonitor GetSpeedupEffectMonitor()
    {
        return Camera.main.GetComponent<SpeedupEffectMonitor>();
    }
}

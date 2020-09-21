using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager 
{

    #region Fields

    static List<PickupBlock> freezerEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float>> freezerEffectListeners = new List<UnityAction<float>>();

    static List<PickupBlock> speedUpEffectInvokers = new List<PickupBlock>();
    static List<UnityAction<float, float>> speedUpEffectListener = new List<UnityAction<float, float>>();

    static List<Block> pointsAddedInvokers = new List<Block>();
    static List<UnityAction<int>> pointsAddedListeners = new List<UnityAction<int>>();

    static List<Ball> ballsDiedInvokers = new List<Ball>();
    static List<UnityAction> ballsDiedListeners = new List<UnityAction>();

    static List<Ball> ballsLostInvokers = new List<Ball>();
    static List<UnityAction> ballsLostListeners = new List<UnityAction>();

    static List<HUD> lastBallLostInvokers = new List<HUD>();
    static List<UnityAction> lastBallLostListeners = new List<UnityAction>();

    static List<Block> blockDestroyedInvokers = new List<Block>();
    static List<UnityAction> blockDestroydeListeners = new List<UnityAction>();
    #endregion

    #region Methods

    public static void AddFreezerEffectInvoker(PickupBlock invoker)
    {
        freezerEffectInvokers.Add(invoker);
        foreach (UnityAction<float> listener in freezerEffectListeners)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectListeners.Add(listener);
        foreach (PickupBlock invoker in freezerEffectInvokers)
        {
            invoker.AddFreezerEffectListener(listener);
        }
    }

    public static void AddSpeedUpEffectInvoker(PickupBlock invoker)
    {
        speedUpEffectInvokers.Add(invoker);
        foreach(UnityAction<float, float> listener in speedUpEffectListener)
        {
            invoker.AddSpeedUpEffectListener(listener);
        }
    }
    public static void AddSpeedUpEffectListener(UnityAction<float, float> listener)
    {
        speedUpEffectListener.Add(listener);
        foreach (PickupBlock invoker in speedUpEffectInvokers)
        {
            invoker.AddSpeedUpEffectListener(listener);
        }
    }

    public static void AddPointsAddedInvoker(Block invoker)
    {
        pointsAddedInvokers.Add(invoker);
        foreach (UnityAction<int> listener in pointsAddedListeners)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    public static void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedListeners.Add(listener);
        foreach (Block invoker in pointsAddedInvokers)
        {
            invoker.AddPointsAddedListener(listener);
        }
    }

    public static void AddBallDiedInvoker(Ball invoker)
    {
        ballsDiedInvokers.Add(invoker);
        foreach(UnityAction listener in ballsDiedListeners)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    public static void AddBallDiedListener(UnityAction listener)
    {
        ballsDiedListeners.Add(listener);
        foreach(Ball invoker in ballsDiedInvokers)
        {
            invoker.AddBallDiedListener(listener);
        }
    }

    public static void AddBallLostInvoker(Ball invoker)
    {
        ballsLostInvokers.Add(invoker);
        foreach (UnityAction listener in ballsLostListeners)
        {
            invoker.AddBallLostListener(listener);
        }
    }

    public static void AddBallLostListener(UnityAction listener)
    {
        ballsLostListeners.Add(listener);
        foreach (Ball invoker in ballsLostInvokers)
        {
            invoker.AddBallLostListener(listener);
        }
    }

    public static void AddLastBallLostInvoker(HUD invoker)
    {
        lastBallLostInvokers.Add(invoker);
        foreach (UnityAction listener in lastBallLostListeners)
        {
            invoker.AddLastBallLostListener(listener);
        }
    }

    public static void AddLastBallLostListener(UnityAction listener)
    {
        lastBallLostListeners.Add(listener);
        foreach(HUD invoker in lastBallLostInvokers)
        {
            invoker.AddLastBallLostListener(listener);
        }
    }

    public static void AddBlockDestroyedInvoker(Block invoker)
    {
        blockDestroyedInvokers.Add(invoker);
        foreach (UnityAction listener in blockDestroydeListeners)
        {
            invoker.AddBlockDestroyedListener(listener);
        }
    }

    public static void AddBlockDestroyedListener(UnityAction listener)
    {
        blockDestroydeListeners.Add(listener);
        foreach (Block invoker in blockDestroyedInvokers)
        {
            invoker.AddBlockDestroyedListener(listener);
        }
    }


    #endregion

}

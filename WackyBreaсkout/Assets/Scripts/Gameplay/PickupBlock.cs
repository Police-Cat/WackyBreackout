using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A pickup block
/// </summary>
public class PickupBlock : Block
{
    [SerializeField] private Sprite freezerSprite;
    [SerializeField] private Sprite speedupSprite;

    private FreezerEffectActivated freezerEffectActivated;
    private SpeedUpEffectActivated speedUpEffectActivated;

    private PickupEffect effect;
    private float EffectDuration;
    private float speedUpScale;


    /// <summary>
    /// Sets the effect for the pickup
    /// </summary>
    /// <value>pickup effect</value>
    public PickupEffect Effect
    {
        set
        {
            effect = value;

            // set sprite
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (effect == PickupEffect.Freezer)
            {
                spriteRenderer.sprite = freezerSprite;
                EffectDuration = ConfigurationUtils.FreezerEffectDuration;
                freezerEffectActivated = new FreezerEffectActivated();
                EventManager.AddFreezerEffectInvoker(this);
            }
            else
            {
                spriteRenderer.sprite = speedupSprite;
                EffectDuration = ConfigurationUtils.SpeedUpEffectDuration;
                speedUpScale = ConfigurationUtils.SpeedUpScale;
                speedUpEffectActivated = new SpeedUpEffectActivated();
                EventManager.AddSpeedUpEffectInvoker(this);
            }
        }
    }

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    override protected void Start()
	{
        // set points
        points = ConfigurationUtils.PickupBlockPoints;

        base.Start();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (effect == PickupEffect.Freezer)
        {
            freezerEffectActivated.Invoke(EffectDuration);
        }
        else if (effect == PickupEffect.Speedup)
        {
            speedUpEffectActivated.Invoke(EffectDuration, speedUpScale);
        }


        base.OnCollisionEnter2D(collision);
    }

    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectActivated.AddListener(listener);
    }

    public void AddSpeedUpEffectListener(UnityAction<float, float> listener)
    {
        speedUpEffectActivated.AddListener(listener);
    }



    #endregion
}

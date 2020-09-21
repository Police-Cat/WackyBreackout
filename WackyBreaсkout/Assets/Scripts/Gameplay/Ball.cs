using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A ball
/// </summary>
public class Ball : MonoBehaviour
{
    // move delay timer
    Timer moveTimer;

    // death timer
    Timer deathTimer;

    // speed up timer
    Timer speedUpTimer;

    private Rigidbody2D rigidbody2D;
    private float speedUpScale;

    private BallDied ballDied;
    private BallLost ballLost;


	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // start move timer
        moveTimer = gameObject.AddComponent<Timer>();
        moveTimer.AddTimerFinishedListener(HandleMoveTimerFInished);
        moveTimer.Duration = 1;
        moveTimer.Run();

        // start death timer
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.AddTimerFinishedListener(HandleDeathTimerFinished);
        deathTimer.Duration = ConfigurationUtils.BallLifeSeconds;
        deathTimer.Run();


        // work with speed up timer
        speedUpTimer = gameObject.AddComponent<Timer>();
        speedUpTimer.AddTimerFinishedListener(HandleSpeedUpTimerFinished);
        EventManager.AddSpeedUpEffectListener(HandleSpeedUpEffectActivated);

        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();


        ballDied = new BallDied();
        ballLost = new BallLost();
        EventManager.AddBallDiedInvoker(this);
        EventManager.AddBallLostInvoker(this);

    }

    /// <summary>
    /// Spawn new ball and destroy self when out of game
    /// </summary>
    void OnBecameInvisible()
    {
        // death timer destruction is in Update
        if (!deathTimer.Finished)
        {
            
            // only spawn a new ball if below screen
            float halfColliderHeight = gameObject.GetComponent<BoxCollider2D>().size.y / 2;
            if (transform.position.y - halfColliderHeight < ScreenUtils.ScreenBottom)
            {
                AudioManager.Play(AudioClipName.BallLost);
                ballLost.Invoke();
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Starts the ball moving
    /// </summary>
    void StartMoving()
    {
        // get the ball moving
        float angle = -90 * Mathf.Deg2Rad;
        Vector2 force = new Vector2(
            ConfigurationUtils.BallImpulseForce * Mathf.Cos(angle),
            ConfigurationUtils.BallImpulseForce * Mathf.Sin(angle));

        if (EffectUtils.SpeedupEffectActive)
        {
            StartSpeedupEffect(EffectUtils.SpeedupEffectSecondsLeft, EffectUtils.SpeedUpScale);
            force *= speedUpScale;
        }

        rigidbody2D.AddForce(force);
    }

    /// <summary>
    /// Sets the ball direction to the given direction
    /// </summary>
    /// <param name="direction">direction</param>
    public void SetDirection(Vector2 direction)
    {
        float speed = rigidbody2D.velocity.magnitude;
        rigidbody2D.velocity = direction * speed;
    }

    /// <summary>
    /// Handles the speedup effect activated event
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup factor</param>
    private void HandleSpeedUpEffectActivated(float duration, float speedUpScale)
    {
        if (speedUpTimer.Running == false)
        {
            AudioManager.Play(AudioClipName.SpeedupEffectActivated);
            StartSpeedupEffect(duration, speedUpScale);
            rigidbody2D.velocity *= speedUpScale;
        }
        else
        {
            speedUpTimer.AddTime(duration);
        }
    }

    /// <summary>
    /// Starts the speedup effect
    /// </summary>
    /// <param name="duration">duration of the speedup effect</param>
    /// <param name="speedupFactor">the speedup factor</param>
    private void StartSpeedupEffect(float duration, float speedUpScale)
    {
        this.speedUpScale = speedUpScale;
        speedUpTimer.Duration = duration;
        speedUpTimer.Run();
    }


    public void AddBallDiedListener(UnityAction listener)
    {
        ballDied.AddListener(listener);
    }

    public void AddBallLostListener(UnityAction listener)
    {
        ballLost.AddListener(listener);
    }

    private void HandleMoveTimerFInished()
    {
        moveTimer.Stop();
        StartMoving();
    }

    private void HandleDeathTimerFinished()
    {
        // spawn new ball and destroy self
        ballDied.Invoke();
        Destroy(gameObject);
    }

    private void HandleSpeedUpTimerFinished()
    {
        AudioManager.Play(AudioClipName.SpeedupEffectDeactivated);
        speedUpTimer.Stop();
        rigidbody2D.velocity *= 1 / speedUpScale;
    }

}
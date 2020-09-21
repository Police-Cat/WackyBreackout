using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabBall;

    // spawn support
    private Timer spawnTimer;
    private float spawnRange;

    // collision-free support
    private bool retrySpawn = false;
    private Vector2 spawnLocationMin;
    private Vector2 spawnLocationMax;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // spawn and destroy ball to calculate
        // spawn location min and max
        GameObject tempBall = Instantiate<GameObject>(prefabBall);
        BoxCollider2D collider = tempBall.GetComponent<BoxCollider2D>();
        float ballColliderHalfWidth = collider.size.x / 2;
        float ballColliderHalfHeight = collider.size.y / 2;

        spawnLocationMin = new Vector2(
            tempBall.transform.position.x - ballColliderHalfWidth,
            tempBall.transform.position.y - ballColliderHalfHeight);

        spawnLocationMax = new Vector2(
            tempBall.transform.position.x + ballColliderHalfWidth,
            tempBall.transform.position.y + ballColliderHalfHeight);

        Destroy(tempBall);

        // initialize and start spawn timer
        spawnRange = ConfigurationUtils.MaxSpawnSeconds - ConfigurationUtils.MinSpawnSeconds;

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.AddTimerFinishedListener(HandleSpawnTimerFinished);
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();

        EventManager.AddBallDiedListener(SpawnBall);
        EventManager.AddBallLostListener(SpawnBall);


        // spawn first Ball
        SpawnBall();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

        // try again if spawn still pending
        if (retrySpawn)
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        // don't stack with a spawn still pending
        retrySpawn = false;
        // make sure we don't spawn into a collision
        if (Physics2D.OverlapArea(spawnLocationMin, spawnLocationMax) == null)
        {
            AudioManager.Play(AudioClipName.BallSpawn);
            retrySpawn = false;
            Instantiate(prefabBall);
        }
        else
        {
            retrySpawn = true;
        }
    }

    /// <summary>
    /// Gets the spawn delay in seconds for the next ball spawn
    /// </summary>
    /// <returns>spawn delay</returns>
    float GetSpawnDelay()
    {
        return ConfigurationUtils.MinSpawnSeconds + Random.value * spawnRange;
    }

    private void HandleSpawnTimerFinished()
    {
        // don't stack with a spawn still pending
        retrySpawn = false;
        SpawnBall();
        spawnTimer.Duration = GetSpawnDelay();
        spawnTimer.Run();
    }
}

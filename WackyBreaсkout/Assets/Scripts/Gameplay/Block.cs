using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A block
/// </summary>
public class Block : MonoBehaviour
{
	PointsAdded pointsAdded;
    protected int points;

	private BlockDestroyed blockDestroyed;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	virtual protected void Start()
	{
		pointsAdded = new PointsAdded();
		EventManager.AddPointsAddedInvoker(this);

		blockDestroyed = new BlockDestroyed();
		EventManager.AddBlockDestroyedInvoker(this);
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		
	}

    /// <summary>
    /// Destroys the block on collision with a ball
    /// </summary>
    /// <param name="coll">Coll.</param>
    virtual protected void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
			AudioManager.Play(AudioClipName.BallCollision);
			pointsAdded.Invoke(points);
			blockDestroyed.Invoke();
			Destroy(gameObject);
        }
    }

	public void AddPointsAddedListener(UnityAction<int> listener)
    {
		pointsAdded.AddListener(listener);
    }

	public void AddBlockDestroyedListener(UnityAction listener)
    {
		blockDestroyed.AddListener(listener);
    }
}

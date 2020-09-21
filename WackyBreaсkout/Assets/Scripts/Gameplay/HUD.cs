using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// The HUD for the game
/// </summary>
public class HUD : MonoBehaviour
{
	#region Fields

	// score support
	static Text scoreText;
	static int score;
    const string ScorePrefix = "Score: ";

    // balls left support
    static Text ballsLeftText;
    static int ballsLeft;
    const string BallsLeftPrefix = "Balls Left: ";

    private LastBallLost lastBallLost;

    private int highScore;

    #endregion

    public int Score
    {
        get { return score; }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        // initialize score text
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		scoreText.text = ScorePrefix + score;

        // initialize balls left value and text
        ballsLeft = ConfigurationUtils.BallsPerGame;
        ballsLeftText = GameObject.FindGameObjectWithTag("BallsLeftText").GetComponent<Text>();
        ballsLeftText.text = BallsLeftPrefix + ballsLeft;

        EventManager.AddPointsAddedListener(AddPoints);
        EventManager.AddBallLostListener(ReduceBallsLeft);

        lastBallLost = new LastBallLost();
        EventManager.AddLastBallLostInvoker(this);

        // Take high score
        highScore = PlayerPrefs.GetInt("HighScore");
	}

	#region Public methods

	/// <summary>
	/// Updates the score
	/// </summary>
	/// <param name="points">points to add</param>
	private void AddPoints(int points)
    {
		score += points;
		scoreText.text = ScorePrefix + score;
        if(score > highScore)
        {
            SaveHighScore(score);
        }
	}

    /// <summary>
    /// Updates the balls left
    /// </summary>
    private void ReduceBallsLeft()
    {
        ballsLeft--;
        ballsLeftText.text = BallsLeftPrefix + ballsLeft;
        if (ballsLeft <= 0)
        {
            lastBallLost.Invoke();
        }
    }

    public void AddLastBallLostListener(UnityAction listener)
    {
        lastBallLost.AddListener(listener);
    }

    void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    #endregion
}

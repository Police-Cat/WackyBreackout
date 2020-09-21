using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{

    [SerializeField] Text gameOverScore;
    [SerializeField] Text highScoreText;

    private float highScore;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = $"High Score: {highScore}";
    }



    /// <summary>
    /// Sets the score in the message to the given score
    /// </summary>
    /// <param name="score">core</param>
    public void SetScore(int score)
    {
        gameOverScore.text = $"Score: {score}";

        if (highScore == default)
        {
            highScoreText.text = $"High Score: {score}";
        }
    }

    public void HanldeQuitButtonOnClick()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);

    }
}

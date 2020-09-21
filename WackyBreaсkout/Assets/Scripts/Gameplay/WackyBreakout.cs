using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WackyBreakout : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddLastBallLostListener(HandleLastBallLost);
        EventManager.AddBlockDestroyedListener(HandleBlockDestroyed);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuManager.GoToMenu(MenuName.Pause);
        }
    }

    private void HandleLastBallLost()
    {
        AudioManager.Play(AudioClipName.GameLost);
        GameOver();
    }

    private void HandleBlockDestroyed()
    {
        if (GameObject.FindObjectsOfType<Block>().Length == 1)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        // instantiate prefab and set score
        GameObject gameOverMessage = Instantiate(Resources.Load("GameOverMenu")) as GameObject;
        GameOverMenu gameOverMessageScript = gameOverMessage.GetComponent<GameOverMenu>();
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        HUD hudScript = hud.GetComponent<HUD>();
        gameOverMessageScript.SetScore(hudScript.Score);
    }

}

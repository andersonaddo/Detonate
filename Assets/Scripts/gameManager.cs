using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    public int bombCooldown, gameLevelIndex, numberOfBombs;
    int currentCooldown;
    public List<bomb> bombs;
    public static gameManager Instance;
    public GameObject pausePanel;

    public List<dungeonColorScheme> colorSchemes;
    public dungeonColorScheme chosenScheme;

    bool gameOver = false;

    public PathFinder pathFinder;
    public Text bombsLeftText, timeLeftText;

    public GameObject gameOverPanel, successText, failureText;

    //Making the gameobjech easy to access externally
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentCooldown = bombCooldown;
            timeLeftText.text = "" + currentCooldown;
            StartCoroutine("countDown");

            chosenScheme = colorSchemes[Random.Range(0, colorSchemes.Count)];
            Camera.main.backgroundColor = chosenScheme.backgroundColor;
        }
    }

    IEnumerator countDown()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);
            currentCooldown--;
            timeLeftText.text = "" + currentCooldown;
            if (currentCooldown <= 0)
            {
                foreach (bomb bomb in bombs)
                {
                    bomb.detonate();
                }
                endGame(true);
            }
        }
    }

    public void updateBombsLeft(int change)
    {
        numberOfBombs += change;
        bombsLeftText.text = "" + numberOfBombs;
        if (numberOfBombs == 0) endGame(false);
        currentCooldown = bombCooldown;
    }

    public void endGame(bool hasFailed)
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

        if (hasFailed) failureText.SetActive(true);
        else successText.SetActive(true);
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void unpauseGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}

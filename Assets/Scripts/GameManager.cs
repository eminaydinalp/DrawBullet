using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject confetti, levelPassed, gameOver, ingamesScreen;
    public bool isFinish;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isFinish)
        {
            ingamesScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        Debug.Log("restrat");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    
    public void Fail()
    {
        isFinish = true;
        StartCoroutine(nameof(GameOver));
    }
    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(.1f);
        ingamesScreen.SetActive(false);
        gameOver.SetActive(true);
        levelPassed.SetActive(false);
    }
    
    public void Win()
    {
        isFinish = true;
        StartCoroutine(nameof(CompleteLevel));
    }
    
    IEnumerator KonfettiPatlat()
    {
        yield return new WaitForSeconds(0.3f);
        confetti.SetActive(true);
    }

    public IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(0.1f);
        ingamesScreen.SetActive(false);
        StartCoroutine(KonfettiPatlat());
        levelPassed.SetActive(true);
        gameOver.SetActive(false);
    }
}

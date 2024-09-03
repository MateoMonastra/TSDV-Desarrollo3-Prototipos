using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADMinigame : MonoBehaviour
{
    private Image progressBar;
    public float decreaseRate = 0.1f;
    public float increaseAmount = 0.05f;
    public float maxProgress = 1f;
    public float minProgress = 0f;

    private bool _hasStarted;
    public bool _hasWon;

    public event Action OnWin;
    public event Action OnLose;

    private void Start()
    {
        progressBar = GetComponent<Image>();
        _hasStarted = false;
    }

    private void OnEnable()
    {
        _hasStarted = false;
        decreaseRate = 0.2f;
        increaseAmount = 0.05f;
        maxProgress = 1f;
        minProgress = 0f;
    }

    void Update()
    {
        if (_hasStarted)
        {
            progressBar.fillAmount -= decreaseRate * Time.deltaTime;
        }

        if (_hasStarted && progressBar.fillAmount <= minProgress)
        {
            progressBar.fillAmount = minProgress;
            LoseGame();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            _hasStarted = true;
            progressBar.fillAmount += increaseAmount;
        }


        if (progressBar.fillAmount >= maxProgress)
        {
            progressBar.fillAmount = maxProgress;
            WinGame();
        }
    }

    void WinGame()
    {
        OnWin?.Invoke();
    }

    void LoseGame()
    {
        OnLose?.Invoke();
    }
}


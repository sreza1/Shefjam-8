using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


using Vector3 = UnityEngine.Vector3;

public class ScoreManager : MonoBehaviour
{
	// Events
	public delegate void ScoreIncrement(BigInteger displayedScore, BigInteger newScore, BigInteger scoreDiff);
    public static event ScoreIncrement OnScoreIncrement;

    public delegate void LevelComplete();
    public static event LevelComplete OnLevelComplete;

    public delegate void TimerResume();
    public delegate void TimerPause();
    public static event TimerResume OnTimerResume;
    public static event TimerPause OnTimerPause;

    public static int scorePow = 0;
    private int actualScore = 0;
    public static BigInteger scoreValue = 1;
    public static BigInteger displayedValue;
    public static BigInteger scoreDiff;

    public int maxScore = 15;
    private int winningScore = 0;

    private bool timerRunning = false;
    private float timerValue = 0;

    void Start() {
    	ResumeTimer();
    }
    void Update()
    {
    	if(timerRunning)
    	{
    		timerValue += Time.deltaTime;
    	}
    }

    public void BossDefeated() {
    	Increment();
    	OnLevelComplete();
    }


    public void Increment()
    {
    	print("INCREMENT");
        scorePow += 9;
        actualScore += 1;
        scoreValue = (BigInteger)Math.Pow(2, scorePow);

        scoreDiff = scoreValue - displayedValue;

		//Increase size of font when increment
        OnScoreIncrement(displayedValue, scoreValue, scoreDiff);

        if (finalLevel && actualScore > winningScore) {
        	GameManager.instance.GetScoreManager().PauseTimer();
	        OnLevelComplete();
        }
    }

    public int GetActualScore()
    {
    	return actualScore;
    }

    public void PauseTimer() 
    {
    	timerRunning = false;
    	OnTimerPause();
    }

    public void ResumeTimer()
    {
    	timerRunning = true;
    	OnTimerResume();
    }

    public void ResetTimer()
    {
    	timerValue = 0f;
    }

    public int GetMaxScore() {
    	return maxScore;
    }

    public bool MaxScoreReached() {
    	return actualScore >= maxScore;
    }

    public float GetElapsedTime() {
    	return timerValue;
    }

    private bool finalLevel = false;
    public void SetFinalLevel() {
    	winningScore = actualScore + 6;
    	print("Actual Score:" + actualScore);
    	print("WINNIGN SCORE: " + winningScore);
    	finalLevel = true;
    }

    public void ResetScoreValue()
    {
    	print("RESET PoW");
    	scorePow = 0;
    }
}


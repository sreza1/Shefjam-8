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

    public delegate void TimerResume(bool Restart);
    public delegate void TimerPause(bool Finish);
    public static event TimerResume OnTimerResume;
    public static event TimerPause OnTimerPause;

    public static int scorePow = 0;
    private int actualScore = 0;
    public static BigInteger scoreValue = 1;
    public static BigInteger displayedValue;
    public static BigInteger scoreDiff;


    private bool timerRunning = false;
    private float timerValue = 0;
    private bool startTimerWhenReady = false;


    void Update()
    {
    	if(!timerRunning && startTimerWhenReady) {
    		ResumeTimer();
    	}

    	if(timerRunning)
    	{
    		timerValue += Time.deltaTime;
    	}
    }

    public void Increment()
    {
        scorePow += 9;
        actualScore += 1;
        scoreValue = (BigInteger)Math.Pow(2, scorePow);

        scoreDiff = scoreValue - displayedValue;

		//Increase size of font when increment
        OnScoreIncrement(displayedValue, scoreValue, scoreDiff);
    }

    public int GetActualScore()
    {
    	return actualScore;
    }

    public void StartTimer() {
    	print("timer started");
    	startTimerWhenReady = true;
    }

    public void PauseTimer() 
    {
    	print("Score timer paused");
    	timerRunning = false;
    	OnTimerPause(false);
    }

    public void ResumeTimer()
    {
    	print("Score tiemr resumed");
    	timerRunning = true;
    	OnTimerResume(false);
    }

    public void StopTimer() // end timer
    {
    	timerRunning = false;
    	OnTimerPause(true);
    }
    
}


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

    public static int scorePow = 0;
    private int actualScore = 0;
    public static BigInteger scoreValue = 1;
    public static BigInteger displayedValue;
    public static BigInteger scoreDiff;


    void Update()
    {
    	// score event
        if (Input.GetKeyDown("space"))
        {
            Increment();
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
    
}


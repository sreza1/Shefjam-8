using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

using Vector3 = UnityEngine.Vector3;

public class UIManager : MonoBehaviour
{

	//******* SCORE RELATED STUFF *****//
	private int actualScore = 0;
	private BigInteger scoreDiff = 0;
	private BigInteger displayedValue = 0;
	private BigInteger scoreValue = 0;
	public static Text score;
    public static Image inf;
    public static Text addedScore;
    public static Vector3 textStartPos;
    public static float textOffset;

    public static Color textColor;

    public static int startFontSize;
    public static float floatFontSize;
    public static Vector3 startImageScale;

    public static int maxScore = 2;


    private static Text timer;
    private float timerValue = 0.0f;
    private bool timerRunning = false;

    private static Text health;
    
	// Start is called before the first frame update
    void Start()
    {	
    	ScoreManager.OnScoreIncrement += Increment;
    	ScoreManager.OnTimerResume += TimerResume;
    	ScoreManager.OnTimerPause += TimerPause;
    	PlayerManager.OnHealthChanged += HealthChanged;

        score = transform.GetChild(0).GetComponent<Text>();

        addedScore = GameObject.Find("Added Score").GetComponent<Text>();
        inf = GameObject.Find("Infinity").GetComponent<Image>();
        inf.gameObject.SetActive(false);

        textColor = addedScore.color;
        textColor.a = 0.0f;
        addedScore.color = textColor;

        textStartPos = addedScore.transform.position;
        startFontSize = score.fontSize;
        floatFontSize = score.fontSize;

        startImageScale = inf.transform.localScale;


        timer = transform.GetChild(1).GetComponent<Text>();
        health = transform.GetChild(2).GetComponent<Text>();
        GameManager.instance.GetPlayerManager().InitUIHealth();
        
        TimerResume(); // start timer once UI loads
    }

    // Update is called once per frame
    void Update()
    {
        if (actualScore < maxScore)
        {
            UpdateScoreValues();
            UpdateTextUI();
        }
        else if (actualScore >= maxScore && inf.gameObject.active == false)
        {
            // score.gameObject.SetActive(false);
            score.text = "";
            addedScore.gameObject.SetActive(false);
            inf.gameObject.SetActive(true);
            inf.transform.localScale = startImageScale*2;
        }
        else
        {
            if (inf.transform.localScale.sqrMagnitude > startImageScale.sqrMagnitude) {
                inf.transform.localScale -= Time.deltaTime*startImageScale*10;
            }
        }

        if (timerRunning) 
        {
        	timerValue += Time.deltaTime;
        	UpdateTimerText();
        }
    }

    void UpdateScoreValues()
    {
        if (displayedValue <= scoreValue)
        {
            displayedValue += scoreDiff / (int)(1 / Time.deltaTime);
        }

        //Shrinks size of font
        if (score.fontSize > startFontSize)
        {
            floatFontSize -= Time.deltaTime*30*4;
            score.fontSize = (int)floatFontSize;
        }

        if (textOffset < 0)
        {
            var newPos = textStartPos;
            newPos.y += textOffset;
            addedScore.transform.position = newPos;
            textOffset += Time.deltaTime * 60;

            textColor.a = Math.Abs(textOffset) / 50.0f;
            addedScore.color = textColor;
        }
        else
        {
            textColor.a = 0.0f;
        }
        addedScore.color = textColor;
    }

    void UpdateTextUI()
    {
        score.text = String.Format("{0:n0}", displayedValue);
        addedScore.text = "+ " + String.Format("{0:n0}", scoreDiff);
    }

    void Increment(BigInteger displayedScore, BigInteger newScore, BigInteger scoreDiff) {
    	this.scoreDiff = scoreDiff;
    	displayedValue = displayedScore;
    	scoreValue = newScore;
    	//Increase size of font when increment
        score.fontSize = (int)(startFontSize * 2);
        floatFontSize = score.fontSize;

        textOffset = -50;
        textColor.a = 1.0f;
        addedScore.color = textColor;

        actualScore += 1;
    }

    /***** END OF SCORE RELATED STUFF ****/

    /*** TIMER ***/
    void TimerPause() {
    	timerRunning = false;
    }

    void TimerResume() {
    	print("timer resumed");
    	timerRunning = true;
    	timerValue = 0.0f;
    }

    void UpdateTimerText()
    {	
    	TimeSpan timespan = TimeSpan.FromSeconds((double)timerValue);
		timer.text = timespan.ToString(@"mm\:ss");
    }

    //** END OF TIMER **//

    // ** HEALTH **//
    void HealthChanged(int newHealth)
    {
    	health.text = newHealth.ToString();
    }
}

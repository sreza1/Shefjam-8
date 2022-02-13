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
    public static Text addedScore;
    public static Vector3 textStartPos;
    public static float textOffset;

    public static Color textColor;

    public static int startFontSize;
    public static float floatFontSize;

    public static int maxScore = 30;

	// Start is called before the first frame update
    void Start()
    {
    	ScoreManager.OnScoreIncrement += Increment;
        score = transform.GetChild(0).GetComponent<Text>();

        addedScore = GameObject.Find("Added Score").GetComponent<Text>();

        textColor = addedScore.color;
        textColor.a = 0.0f;
        addedScore.color = textColor;

        textStartPos = addedScore.transform.position;
        startFontSize = score.fontSize;
        floatFontSize = score.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualScore < maxScore)
        {
            UpdateScoreValues();
            UpdateTextUI();
        }
        else
        {
        	score.text = "Infinty";
            //score.gameObject.SetActive(false);
            addedScore.gameObject.SetActive(false);
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
}

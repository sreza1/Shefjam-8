using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

using Vector3 = UnityEngine.Vector3;

public class ScoreManager : MonoBehaviour
{
    public static int scorePow = 0;
    public static BigInteger scoreValue = 1;
    public static BigInteger displayedValue;
    public static BigInteger scoreDiff;

    public static int maxScore = 30;

    public static Text score;
    public static Text addedScore;
    public static int startFontSize;
    public static Vector3 textStartPos;
    public static int textOffset;

    public static Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();

        addedScore = GameObject.Find("Added Score").GetComponent<Text>();

        textColor = addedScore.color;
        textColor.a = 0.0f;
        addedScore.color = textColor;

        textStartPos = addedScore.transform.position;
        startFontSize = score.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (scorePow / 9 < maxScore)
        {
            // score event
            if (Input.GetKeyDown("space"))
            {
                Increment();
            }

            UpdateScoreValues();
            UpdateTextUI();
        }
        else
        {
            score.gameObject.SetActive(false);
            addedScore.gameObject.SetActive(false);
        }
    }

    void Increment()
    {
        scorePow += 9;
        scoreValue = (BigInteger)Math.Pow(2, scorePow);

        scoreDiff = scoreValue - displayedValue;

        //Increase size of font when increment
        score.fontSize = (int)(startFontSize * 2);

        textOffset = -50;
        textColor.a = 1.0f;
        addedScore.color = textColor;
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
            score.fontSize -= (int)(Time.deltaTime*350);
        }

        if (textOffset < 0)
        {
            var newPos = textStartPos;
            newPos.y += textOffset;
            addedScore.transform.position = newPos;
            textOffset += 2;

            textColor.a = Math.Abs(textOffset) / 50.0f;
            addedScore.color = textColor;
        }
    }

    void UpdateTextUI()
    {
        score.text = String.Format("{0:n0}", displayedValue);
        addedScore.text = "+ " + String.Format("{0:n0}", scoreDiff);
    }
}


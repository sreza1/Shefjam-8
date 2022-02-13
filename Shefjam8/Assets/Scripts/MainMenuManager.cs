using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
	public int mainTitleStartFontSize;
    public int mainTitleMaxFontSize;
    private float mainTitleCurrentFontSize;

    [SerializeField] public GameObject mainTitle;
    private Text mainTitleText;
    private bool mainTitleSizeChangeDirection = true; // true up, false down
    public float mainTitleSizeChangeSpeed = 10.0f;


    public int promptStartFontSize;
    public int promptMaxFontSize;
    private float promptCurrentFontSize;

    [SerializeField] public GameObject prompt;
    private Text promptText;
    private bool promptSizeChangeDirection = true; // true up, false down
    public float promptSizeChangeSpeed = 10.0f;


    // Start is called before the first frame update

    void Start()
    {
    	mainTitleText = mainTitle.GetComponent<Text>();   
    	mainTitleCurrentFontSize = mainTitleText.fontSize;
    	promptText = prompt.GetComponent<Text>();
    	promptCurrentFontSize = promptText.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
    	UpdateMainTitleSize();
    	UpdatePromptSize();
    	if (Input.anyKey)
        {
        	SceneManager.LoadScene("FirstFloor", LoadSceneMode.Single);
        }
    }

    void UpdateMainTitleSize()
    {
    	//Shrinks size of font
        if (mainTitleSizeChangeDirection)
        { 
        	if(mainTitleText.fontSize < mainTitleMaxFontSize)
	        {
	            mainTitleCurrentFontSize += Time.deltaTime*mainTitleSizeChangeSpeed;
	        }
	        else {mainTitleSizeChangeDirection = false;}
    	}
        else if(!mainTitleSizeChangeDirection)
        {
        	if(mainTitleText.fontSize > mainTitleStartFontSize)
        	{
        		mainTitleCurrentFontSize -= Time.deltaTime*mainTitleSizeChangeSpeed;
        	}
        	else
        	{	mainTitleSizeChangeDirection = true; 	}
    	}
    	mainTitleText.fontSize = (int)mainTitleCurrentFontSize;
    }

    void UpdatePromptSize()
    {
    	//Shrinks size of font
        if (promptSizeChangeDirection)
        { 
        	if(promptText.fontSize < promptMaxFontSize)
	        {
	            promptCurrentFontSize += Time.deltaTime*promptSizeChangeSpeed;
	        }
	        else {promptSizeChangeDirection = false;}
    	}
        else if(!promptSizeChangeDirection)
        {
        	if(promptText.fontSize > promptStartFontSize)
        	{
        		promptCurrentFontSize -= Time.deltaTime*promptSizeChangeSpeed;
        	}
        	else
        	{	promptSizeChangeDirection = true; 	}
    	}
    	promptText.fontSize = (int)promptCurrentFontSize;
    }
}

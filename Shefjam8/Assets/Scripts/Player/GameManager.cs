using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    private string levelName = "FirstFloor";

    private void Awake()
    {
        if (instance != null && instance != this) // remove duplicates
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            RespawnPlayers();
            levelName = SceneManager.GetActiveScene().name;
        }
    }

    void Start()
    {
    }

    public PlayerManager GetPlayerManager() {
    	return GetComponent<PlayerManager>();
    }

    public InventoryManager GetInventoryManager() {
    	return GetComponent<InventoryManager>();
    }

    public void IncrementScore() {
    	GetComponent<ScoreManager>().Increment();
    }

    public ScoreManager GetScoreManager() {
    	return GetComponent<ScoreManager>();
    }
    
    public void LoadNewScene(string sceneName) 
    {
    	GetScoreManager().PauseTimer();
    	// go to new scene
    	SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    	SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RespawnPlayers();
        levelName = SceneManager.GetActiveScene().name;
        GetScoreManager().ResetScoreValue();
        GetScoreManager().ResumeTimer();
    }

    private void RespawnPlayers() {
    	GetComponent<PlayerManager>().RespawnPlayers();
    }

    public void RestartLevel() {
    	LoadNewScene(levelName);
    }

    public void NextLevel() {
    	if(levelName == "FirstFloor") { LoadNewScene("SecondFloor");}
    	else if (levelName == "SecondFloor") { LoadNewScene("ThirdFloor"); GetComponent<ScoreManager>().SetFinalLevel();}
        else if (levelName == "ThirdFloor") { LoadNewScene("MainMenu"); GetComponent<ScoreManager>().ResetTimer();}
    }
}

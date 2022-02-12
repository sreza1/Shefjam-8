using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this) // remove duplicates
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            RespawnPlayers();
        }
    }

    public PlayerManager GetPlayerManager() {
    	return GetComponent<PlayerManager>();
    }

    public InventoryManager GetInventoryManager() {
    	return GetComponent<InventoryManager>();
    }

    public void LoadNewScene() 
    {
    	// go to new scene
    	//SceneManager.LoadScene("testScene", LoadSceneMode.Single);
    	SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RespawnPlayers();
    }

    private void RespawnPlayers() {
    	GetComponent<PlayerManager>().RespawnPlayers();
    }
}

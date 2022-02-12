using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this) // remove duplicates
        {
            Destroy(this.gameObject);
        } else if (instance != null && instance == this) 
        {
        	DontDestroyOnLoad(this);
        } else {
            instance = this;
        }
    }

    public PlayerManager GetPlayerManager() {
    	return GetComponent<PlayerManager>();
    }

    public InventoryManager GetInventoryManager() {
    	return GetComponent<InventoryManager>();
    }
}

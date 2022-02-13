using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	[SerializeField] private GameObject playerOnePrefab;
	private GameObject playerOne;
	[SerializeField] private int DEFAULT_HEALTH = 100;
	private int playerOneHealth; // persist health here so we can keep it between levels


	// Events
	public delegate void HealthChanged(int newHealth);
    public static event HealthChanged OnHealthChanged;

	public GameObject GetPlayer() {
		return playerOne;
	}

	public void TeleportToExit(GameObject currentLocationObject, GameObject teleportLocationObject, int teleportOffsetDistance){
		if (currentLocationObject.transform.parent.gameObject != teleportLocationObject.transform.parent.gameObject) {
			currentLocationObject.transform.parent.gameObject.SetActive(false);
			teleportLocationObject.transform.parent.gameObject.SetActive(true);
		}
		playerOne.GetComponent<PlayerMovement>().TeleportToLocation(teleportLocationObject, teleportOffsetDistance);
		
	}

    // Start is called before the first frame update
    void Start()
    { 
    	// set health for each player
    	if (playerOne) {
    		playerOneHealth = DEFAULT_HEALTH;
    		OnHealthChanged(DEFAULT_HEALTH);
    	}
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void InitUIHealth() {
    	OnHealthChanged(playerOneHealth);
    }
    // Damage the given player by the specified (base) amount
    public void DamagePlayer(GameObject victim, int damage){
    	PlayerStats pStats = victim.GetComponent<PlayerStats>();
    	int currHealth = pStats.GetCurrentHealth(); // the players current health

    	currHealth -= damage;
    	if (currHealth < 0) { currHealth = 0; } // do the damage (min cap at 0)
    	playerOneHealth = currHealth;

    	pStats.SetNewHealth(currHealth); // send the new value back to the player
    	OnHealthChanged(currHealth);
    	pStats.StartInvulnerableFrames(); // make player invulnerable for a short times
    }

    public void RespawnPlayers()
    {
    	// respawn each player in new scene (meaning full health)
    	if (playerOnePrefab) {
    		playerOne = Instantiate(playerOnePrefab, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0, 0, 0)));
    		if(playerOne) { // set the health to the last stored value (or full if player was dead)
    			if (playerOneHealth <= 0) {playerOneHealth = DEFAULT_HEALTH; } 
    			playerOne.GetComponent<PlayerStats>().SetNewHealth(playerOneHealth);
    		}
    	}
    }
}

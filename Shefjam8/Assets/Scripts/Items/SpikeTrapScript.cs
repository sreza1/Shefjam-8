using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : ItemBase
{
	[SerializeField] private int damageStrength;
	private List<GameObject> collidingPlayers = new List<GameObject>();

    protected override void PlayerCollided(GameObject player) {
    	StartDamagingPlayer(player);	
    }

    protected override void PlayerStoppedColliding(GameObject player) {
    	StopDamagingPlayer(player);
    }

    // start damaging colliding player
    private void StartDamagingPlayer(GameObject player){
    	GameManager.instance.GetPlayerManager(); // add key to global inventory
    	print("On Trap");
    	collidingPlayers.Add(player);
    }

    // stop damaging non-colliding player
    private void StopDamagingPlayer(GameObject player){
    	GameManager.instance.GetPlayerManager(); // add key to global inventory
    	collidingPlayers.Remove(player);
    	print("Off Trap");
    }

    protected override void Update()
    {
    	foreach(GameObject p in collidingPlayers) {
    		if (p && p.GetComponent<PlayerStats>().CanTakeDamage()) {
    			GameManager.instance.GetPlayerManager().DamagePlayer(p, damageStrength);
    		}
    	}
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : ItemBase
{
    protected override void PlayerCollided(GameObject player) {
    	GameManager.instance.GetInventoryManager().AddKey(); // add key to global inventory
    	print("New Key");
    	Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickupScript : ItemBase
{
    protected override void PlayerCollided(GameObject player) {
    	player.GetComponent<InventoryManager>().AddKey();
    	print("New Key");
    	Destroy(gameObject);
    }
}

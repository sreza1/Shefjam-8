using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
	protected PlayerManager playerManager;
	private GameObject p1 = null;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    	playerManager = GameManager.instance.GetPlayerManager();
        p1 = playerManager.GetPlayer();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

   	// Upon collision with another GameObject, check if it is player one, and destroy self (test)
    private void OnTriggerEnter2D(Collider2D other)
    {
    	GameObject collidingObject = other.gameObject;
    	if (collidingObject == p1) {
    		PlayerCollided(collidingObject);
    	}
    }

    protected virtual void PlayerCollided(GameObject player) {
    	Destroy(gameObject);
    }
}

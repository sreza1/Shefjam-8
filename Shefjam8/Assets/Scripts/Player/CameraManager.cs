using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	protected PlayerManager playerManager;
	private GameObject collidingPlayer;
	private GameObject p1 = null;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameManager.instance.GetPlayerManager();
        p1 = playerManager.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
    	if (collidingPlayer) {
    		Vector3 diff = collidingPlayer.transform.position - transform.position ;
    		diff.z = 0;
    		print("camrea pos: " + transform.position.ToString() + "; playerPos: " + collidingPlayer.transform.position.ToString() + "; diff: " + diff.ToString());
        	transform.position += diff * Time.deltaTime * collidingPlayer.GetComponent<PlayerMovement>().moveSpeed;
	    }
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
    	//Vector2 playerLocRelativeToCenter = transform.position -= player.transform.position;
    	collidingPlayer = player;
    	
    	//print("Pv" + playerVelocity.x + "; pv: " + playerVelocity.y + "; speed: " + playerVelocity.GetSpeed());
    	//transform.position += (new Vector3(playerVelocity.x, playerVelocity.y, 0) * playerVelocity.magnitude);
    }

    // Upon collision with another GameObject, check if it is player one, and destroy self (test)
    private void OnTriggerExit2D(Collider2D other)
    {
    	GameObject collidingObject = other.gameObject;
    	if (collidingObject == p1) {
    		PlayerStoppedColliding(collidingObject);
    	}
    }

    protected virtual void PlayerStoppedColliding(GameObject player) {
    	collidingPlayer = null;
    	print("Player Stopped Colliding With Camera Edge");
    }
}

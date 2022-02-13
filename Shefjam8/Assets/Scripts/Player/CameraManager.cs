using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	protected PlayerManager playerManager;
	private GameObject collidingPlayer;
	private GameObject p1 = null;

	// Movement speed in units per second.
    public float cameraSpeed = 10f;

    // Time when the movement started.
    private float movementStartTime; // time we started moving camera

    private int edgeColliderOffset = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameManager.instance.GetPlayerManager();
        p1 = playerManager.GetPlayer();
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
     	Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
     	GetComponent<BoxCollider2D>().size = new Vector2(maxScreenBounds.x - edgeColliderOffset, maxScreenBounds.y - edgeColliderOffset);
    }

    // Update is called once per frame
    void Update()
    {
    	if (collidingPlayer) {
    		Vector3 newLocation = new Vector3(collidingPlayer.transform.position.x, collidingPlayer.transform.position.y, transform.position.z);
    		float halfDistance = Vector3.Distance(transform.position, newLocation);
    		transform.position = Vector3.MoveTowards(transform.position, newLocation, cameraSpeed * Time.deltaTime);
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
    	collidingPlayer = null;
    	
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
    	collidingPlayer = player;
    }
}

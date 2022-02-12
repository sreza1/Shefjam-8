using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float DEFAULT_TELEPORT_DELAY = 1.0f;
	private float teleportedDelay;
    public float moveSpeed = 99f;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 rotate;
    Vector2 mousePos;
    Vector2 previousRot;

    private bool teleported = false;




    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //ps5 right analog stick rotation
        if (Input.GetAxisRaw("HorizontalTurn")!=0 || Input.GetAxisRaw("VerticalTurn")!=0)
        {
            rotate.x = Input.GetAxisRaw("HorizontalTurn");
            rotate.y = Input.GetAxisRaw("VerticalTurn");
        }

        
    }

    void FixedUpdate()
    {
    	// if we've teleported and still in delay, then don't move
    	if (teleported) {
    		teleportedDelay -= Time.deltaTime;
    		if (teleportedDelay <= 0) {
    			teleported = false;
    		}
    		return;
    	}

    	
	    // figure out min/max bounds in world coordinates
	    Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
	    Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

	     Vector3 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
	     newPos = new Vector3(Mathf.Clamp(newPos.x, minScreenBounds.x + 1, maxScreenBounds.x - 1),
	     						Mathf.Clamp(newPos.y, minScreenBounds.y + 1, maxScreenBounds.y - 1),
	     						newPos.z);

	    rb.MovePosition(newPos);
	 	 	

	    float angle = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg - 90f;
	    rb.rotation = angle;
    }

    public void TeleportToLocation(GameObject teleportLocationObject, int teleportOffsetDistance){
    	teleported = true;
		if (teleportLocationObject) {
			Transform newTransform = teleportLocationObject.transform;
			transform.position = newTransform.position + (teleportOffsetDistance * newTransform.transform.up);
			teleportedDelay = DEFAULT_TELEPORT_DELAY;
		}
		
	}
}

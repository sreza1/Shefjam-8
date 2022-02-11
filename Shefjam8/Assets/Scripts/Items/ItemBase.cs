using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

	[SerializeField] private GameObject eventSystem;
	private PlayerManager playerManager;
	private GameObject p1 = null;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = eventSystem.GetComponent<PlayerManager>();
        p1 = playerManager.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   	 // Upon collision with another GameObject, check if it is player one, and destroy self (test)
    private void OnTriggerEnter2D(Collider2D other)
    {
    	GameObject collidingObject = other.gameObject;

    	if (collidingObject == p1) {
    		Destroy(gameObject);
    	}
    }
}

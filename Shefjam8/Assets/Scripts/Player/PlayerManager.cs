using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	[SerializeField] private GameObject playerOne;
	[SerializeField] private int teleportOffset = 2;

	public GameObject GetPlayer() {
		return playerOne;
	}

	public void TeleportToExit(GameObject teleportLocationObject){
		if (teleportLocationObject) {
			Transform newTransform = teleportLocationObject.transform;
			playerOne.transform.position = newTransform.position + (teleportOffset * newTransform.transform.up);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

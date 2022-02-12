using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

	[SerializeField] private GameObject playerOne;

	public GameObject GetPlayer() {
		return playerOne;
	}

	public void TeleportToExit(GameObject teleportLocationObject, int teleportOffsetDistance){
		if (teleportLocationObject) {
			Transform newTransform = teleportLocationObject.transform;
			playerOne.transform.position = newTransform.position + (teleportOffsetDistance * newTransform.transform.up);
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

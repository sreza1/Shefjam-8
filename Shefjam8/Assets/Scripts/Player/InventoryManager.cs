using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	// keys are global across all players
	[SerializeField] private int NumKeys = 0;


	public void AddKey() {
		NumKeys++;
	}

	public void RemoveKey() {
		NumKeys--;
	}

	public bool HasKey() {
		return NumKeys > 0;
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

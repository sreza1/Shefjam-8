using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	[SerializeField] private float invulnerabilityTime = 2f;
	private float remainingInvulnerableTime = 0f;
	private bool invulnerable = false;

	private int currentHealth = 0;

	public int GetCurrentHealth() {
		return currentHealth;
	}

	public void SetNewHealth(int newHealth) {
		currentHealth = newHealth;
		print("New Health" + currentHealth);
	}

	public void StartInvulnerableFrames() {
		remainingInvulnerableTime = invulnerabilityTime;
		invulnerable = true;
	}

	public bool CanTakeDamage() {
		return !invulnerable;
	}

	void OnCollisionEnter2D(Collision2D collision)
    {
        // if we are hit by a bullet and can take damage, take 10 damage
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
        	if (CanTakeDamage()) {
        		GameManager.instance.GetPlayerManager().DamagePlayer(gameObject, 10);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    	// reduce invincibility time by deltaTime
        if (remainingInvulnerableTime > 0) {
        	remainingInvulnerableTime -= Time.deltaTime;
        }
        else
        {
        	invulnerable = false; // otherwise we can be damaged again
        }
    }
}

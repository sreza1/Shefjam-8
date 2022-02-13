using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBlockWHealth : MonoBehaviour
{
	protected PlayerManager playerManager;
	private GameObject p1 = null;
	[SerializeField] private int damageStrength = 10;

	void Start() 
	{
		playerManager = GameManager.instance.GetPlayerManager();
		p1 = playerManager.GetPlayer();
		GetComponent<AIDestinationSetter>().target = p1.transform;
	}

    public int curHealth = 50;
    public int bulletDmg = 10;

    void OnCollisionEnter2D(Collision2D collision){
        //potentially add death effect animation
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Bullet")){
            curHealth-=bulletDmg;
            if (curHealth==0){
            	GameManager.instance.IncrementScore();
                Destroy(gameObject);
            }
        } 

        if  (collision.gameObject == p1 && p1.GetComponent<PlayerStats>().CanTakeDamage())
        {
        	GameManager.instance.GetPlayerManager().DamagePlayer(collision.gameObject, damageStrength);
        }      

    }
}

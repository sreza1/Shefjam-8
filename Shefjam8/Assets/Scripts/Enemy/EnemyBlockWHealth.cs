using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBlockWHealth : MonoBehaviour
{
	protected PlayerManager playerManager;
	private GameObject p1 = null;
	[SerializeField] private int damageStrength = 10;

    public Animator animator;

	void Start() 
	{
		playerManager = GameManager.instance.GetPlayerManager();
		p1 = playerManager.GetPlayer();
		GetComponent<AIDestinationSetter>().target = p1.transform;
	}

    void Update()
    {
    	if (p1) {
	        animator.SetFloat("Horizontal", p1.transform.position.x - this.transform.position.x);
	    }
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

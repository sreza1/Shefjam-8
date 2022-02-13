using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class boss1 : MonoBehaviour
{
    protected PlayerManager playerManager;
    private GameObject p1 = null;
    [SerializeField] private int damageStrength = 10;
    public bool finalLevel = false;
    public float chargeDelta = 0.5F;

    private float CHARGING_DELAY = 3f;
    private float chargingDelayRemaining;


    void Start()
    {
        chargingDelayRemaining = CHARGING_DELAY;
        playerManager = GameManager.instance.GetPlayerManager();
        p1 = playerManager.GetPlayer();
        GetComponent<AIDestinationSetter>().target = p1.transform;
        //to allow charges to happen
        GetComponent<AIPath>().enabled = false;

    }

    void Update()
    {
        chargingDelayRemaining -= Time.deltaTime;
        if (chargingDelayRemaining <= 0)
        {
            
            Charge();
        }
    }

    public int curHealth = 300;
    public int bulletDmg = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //potentially add death effect animation
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Bullet") && curHealth > 0)
        {
            curHealth -= bulletDmg;
            if (curHealth <= 0)
            {
            	if (!finalLevel) {
	            	GameManager.instance.GetScoreManager().PauseTimer();
	            	GameManager.instance.GetScoreManager().BossDefeated();
	            }
	            else {
	            	print("DAMAGE HIT");
	            	GameManager.instance.IncrementScore();
	            }
                Destroy(gameObject);
            }
        }

        if (collision.gameObject == p1)
        {
            GameManager.instance.GetPlayerManager().DamagePlayer(collision.gameObject, damageStrength);
        }

    }

    public Rigidbody2D rb;
    public float moveSpeed;
    Vector2 movement;


    void Charge()
    {
        Vector2 diffDir = p1.transform.position - rb.transform.position;
        rb.AddForce(diffDir.normalized * moveSpeed, ForceMode2D.Impulse);
        chargingDelayRemaining = CHARGING_DELAY;
    }
}

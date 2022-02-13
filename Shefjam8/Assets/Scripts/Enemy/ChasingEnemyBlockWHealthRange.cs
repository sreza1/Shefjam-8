using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasingEnemyBlockWHealthRange : MonoBehaviour
{
    bool detected;

    public GameObject bulletPrefab;
    public Transform shootPoint;

    //public float shootSpeed = 10f;
    //public float timeToShoot = 1.3f;
    //float originalTime;


    public float bulletForce = 20f;
    //public float fireDelta = 0.5F;
    public float shotOffset = 0.75f;

    private float SHOOTING_DELAY = 1f;
    private float shootDelayRemaining = 0.0F;
    private GameObject detectedPlayer;

    public Animator animator;



    protected PlayerManager playerManager;
    private GameObject p1 = null;
    [SerializeField] private int damageStrength = 10;

    void Start()
    {
        playerManager = GameManager.instance.GetPlayerManager();
        p1 = playerManager.GetPlayer();
        GetComponent<AIDestinationSetter>().target = p1.transform;
    }

    private void Update()
    {
        if (detected)
        {
            shootDelayRemaining -= Time.deltaTime;
            if (shootDelayRemaining <= 0)
            {
                ShootPlayer();
            }
        }

        if (p1) {
	        animator.SetFloat("Horizontal", p1.transform.position.x - this.transform.position.x);
	    }

    }

    public int curHealth = 50;
    public int bulletDmg = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //potentially add death effect animation
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            curHealth -= bulletDmg;
            if (curHealth == 0)
            {
            	GameManager.instance.IncrementScore();
                Destroy(gameObject);
            }
        }

        if (collision.gameObject == p1 && p1.GetComponent<PlayerStats>().CanTakeDamage())
        {
            GameManager.instance.GetPlayerManager().DamagePlayer(collision.gameObject, damageStrength);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            detected = true;
            detectedPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            detected = false;
            detectedPlayer = null;
        }
    }

    private void ShootPlayer()
    {
        Vector2 diffDir = detectedPlayer.transform.position - shootPoint.position;
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(diffDir.normalized * bulletForce, ForceMode2D.Impulse);
        shootDelayRemaining = SHOOTING_DELAY;
    }
}

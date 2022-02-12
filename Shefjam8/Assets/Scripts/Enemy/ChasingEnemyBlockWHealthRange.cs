using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyBlockWHealthRange : MonoBehaviour
{
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
                Destroy(gameObject);
            }
        }

    }
}

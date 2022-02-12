using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        //potentially add death effect animation
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        if (collision.gameObject.CompareTag("Bullet")){
            Destroy(gameObject);
        }        

    }
}

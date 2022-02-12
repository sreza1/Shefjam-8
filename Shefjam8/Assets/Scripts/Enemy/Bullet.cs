using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision){
        //potentially add hit effect animation
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 5f);
        // if (collision.gameObject.CompareTag("Enemy")){
        //     Destroy(collision.gameObject);
        // }
        
        Destroy(gameObject);

    }
}

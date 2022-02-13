using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireDelta = 0.5F;

    private float nextFire = 1f;
    private float myTime = 0.0F;


    // Update is called once per frame
    void Update()
    {
        myTime = myTime + Time.deltaTime;
        if ((Input.GetAxisRaw("HorizontalTurn")!=0 || Input.GetAxisRaw("VerticalTurn")!=0) && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            
            Shoot();

            nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
        
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        SoundManagerScript.PlaySound("fire");
    }
}

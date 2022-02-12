using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 99f;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 rotate;
    Vector2 mousePos;
    Vector2 previousRot;




    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //ps5 right analog stick rotation
        if (Input.GetAxisRaw("HorizontalTurn")!=0 || Input.GetAxisRaw("VerticalTurn")!=0)
        {
            rotate.x = Input.GetAxisRaw("HorizontalTurn");
            rotate.y = Input.GetAxisRaw("VerticalTurn");
        }

        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement*moveSpeed *Time.fixedDeltaTime);

        float angle = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        
    }
}

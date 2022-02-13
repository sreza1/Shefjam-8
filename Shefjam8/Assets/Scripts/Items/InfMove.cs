using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfMove : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(-10,10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0.05f,-0.05f,0f);

        transform.Rotate(new Vector3(0f,0f,speed));

        var newX = transform.position.x;
        var newY = transform.position.y;

        if (newX > 14) 
        {
            newX = -14f;
        }

        if (newY < -7) 
        {
            newY = 7f;
        }

        transform.position = new Vector3(newX,newY,0f);
    }
}

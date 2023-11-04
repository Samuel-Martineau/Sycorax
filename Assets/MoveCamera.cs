using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float rotationSpeed;
    private float x;
    private float y;
    private Vector3 rotateValue;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }


        //    if(Input.GetKey(KeyCode.L))
        //    {
        //     //transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        //     transform.Rotate(0,1*rotationSpeed*Time.deltaTime,0);
        //    }
        //    if(Input.GetKey(KeyCode.J))
        //    {
        //     //transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        //     transform.Rotate(0,-1*rotationSpeed*Time.deltaTime,0);
        //    }
        //    if(Input.GetKey(KeyCode.I))
        //    {
        //     //transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        //     transform.Rotate(-1*rotationSpeed*Time.deltaTime,0,0);
        //    }
        //    if(Input.GetKey(KeyCode.K))
        //    {
        //     //transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
        //     transform.Rotate(1*rotationSpeed*Time.deltaTime,0,0);
        //    }

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        //Debug.Log(x + ":" + y);
        rotateValue = new Vector3(x, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;

    }
}

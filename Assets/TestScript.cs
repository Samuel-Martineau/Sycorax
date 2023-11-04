using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float mass;
    public Vector3 velocity;
    public Vector3 acceleration;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        velocity += Time.deltaTime * acceleration;
        transform.position += Time.deltaTime * velocity;
    }
}

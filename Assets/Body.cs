using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float mass = 100;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public bool hasCollided = false;
    public float radius
    {
        get { return Mathf.Pow(mass, 1f / 3f); }
    }

    public Vector3 Momentum
    {
        get { return velocity * mass; }
    }

    public Vector3 AngularMomentum
    {
        get { return 2 / 5 * mass * Mathf.Pow(radius, 2) * angularVelocity; }
    }
    void Start()
    {
        float scale = radius / 1500;
        transform.localScale = new(scale, scale, scale);
    }

    void OnTriggerEnter(Collider collider)
    {
        Body otherBody = collider.gameObject.GetComponent<Body>();
        otherBody.hasCollided = true;

        if (!hasCollided)
        {
            Destroy(collider.gameObject);
            Vector3 totalMomentum = Momentum + otherBody.Momentum;
            Vector3 totalAngularMomentum = AngularMomentum + otherBody.AngularMomentum;
            mass += otherBody.mass;
            velocity = totalMomentum / mass;
            angularVelocity = 5 / 2 * totalAngularMomentum / Mathf.Pow(radius, 2) / mass;
            Debug.Log(mass);
            Start();
        }
    }

    void Update()
    {
        Quaternion Δrotation = Quaternion.Euler(angularVelocity * Time.deltaTime);
        transform.rotation = Δrotation * transform.rotation;
    }


}

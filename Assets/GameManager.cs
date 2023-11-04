using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject body;

    public float adjustmentFactor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        IEnumerable<Body> bodies = FindObjectsOfType<Body>();

        int iterCount = 500;
        for (int i = 0; i < iterCount; i++)
        {
            float Δtime = Time.deltaTime / iterCount;

            IEnumerable<Vector3> accelerations = bodies.Select(self =>
            {
                Vector3 acceleration = bodies.Select(other =>
                {
                    if (self == other) return Vector3.zero;
                    Vector3 difference = other.transform.position - self.transform.position;
                    float magnitude = Constants.G * other.mass / Mathf.Pow(difference.magnitude, 2);
                    Vector3 orientation = difference / difference.magnitude;
                    return magnitude * orientation;
                }).Aggregate(Vector3.zero, (acc, vec) => acc + vec);
                return acceleration;
            });

            foreach (var (b, a) in bodies.Zip(accelerations, (b, a) => (b, a)))
            {
                b.velocity += a * Δtime;
                b.transform.position += b.velocity * Δtime;
            };
        }

        // Vector3 momentum = bodies.Aggregate(Vector3.zero, (acc, b) => acc + b.momentum);
    }

    public void AddBody()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log(mousePosition); //make it spawn in front of camera instead
        mousePosition.z += adjustmentFactor;
        Instantiate(body, mousePosition, Quaternion.identity);
    }
}

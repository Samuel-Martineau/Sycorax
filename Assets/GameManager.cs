using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bodyPrefab;

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
            float Δtime = Time.deltaTime / iterCount / Constants.ζ;

            IEnumerable<Vector3> accelerations = bodies.Select(self =>
            {
                Vector3 acceleration = bodies.Select(other =>
                {
                    if (self == other) return Vector3.zero;
                    Vector3 difference = other.position - self.position;
                    float magnitude = Constants.G * other.mass / Mathf.Pow(difference.magnitude, 2);
                    Vector3 orientation = difference / difference.magnitude;
                    return magnitude * orientation;
                }).Aggregate(Vector3.zero, (acc, vec) => acc + vec);
                return acceleration;
            });

            foreach (var (b, a) in bodies.Zip(accelerations, (b, a) => (b, a)))
            {
                b.velocity += a * Δtime;
                b.position += b.velocity * Δtime;
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
        Instantiate(bodyPrefab, mousePosition, Quaternion.identity);
    }

    public void AddBody(float mass, Vector3 position, Vector3 velocity)
    {
        GameObject gameObject = Instantiate(bodyPrefab, position, Quaternion.identity);
        Body body = gameObject.GetComponent<Body>();
        body.mass = mass;
        body.transform.position = position;
        body.velocity = velocity;
    }
}

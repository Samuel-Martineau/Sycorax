using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameMode
{
    Sandbox = 0,
    SolarSystem = 1
}

public class GameManager : MonoBehaviour
{
    public GameObject bodyPrefab;

    public float adjustmentFactor;

    public float constantG = 6.6743e-11f; // m³ kg² / s² -- Gravitation
    public float constantρ = 1; // kg / m³ -- Density
    public float constantκ = 1e10f; // m / u -- Length Scale
    // public float constantμ = 1e0f; // -- Length treshold
    public float constantλ = 300; // m / u -- Size Scale
    public float constantζ = 1e5f; // s / t -- Time Scale
    public float constantε = 1000;

    public GameMode gameMode = GameMode.Sandbox;
    public bool logScale = false;

    // Start is called before the first frame update
    void Start()
    {
        if (gameMode == GameMode.SolarSystem) StartCoroutine(GetComponent<LivePlanetDataFetcher>().Load());
    }

    public void test()
    {
        StartCoroutine(GetComponent<LivePlanetDataFetcher>().Load());
    }

    // Update is called once per frame
    void Update()
    {

        IEnumerable<Body> bodies = FindObjectsOfType<Body>();

        for (int i = 0; i < constantε; i++)
        {
            float Δtime = Time.deltaTime / constantε * constantζ;

            IEnumerable<Vector3> accelerations = bodies.Select(self =>
            {
                Vector3 acceleration = bodies.Select(other =>
                {
                    if (self == other) return Vector3.zero;
                    Vector3 difference = other.position - self.position;
                    float magnitude = constantG * other.mass / Mathf.Pow(difference.magnitude, 2);
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

        // Vector3 momentum = bodies.Aggregate(Vector3.zero, (acc, b) => acc + b.Momentum);
        // Debug.Log(Mathf.Log10(momentum.magnitude));
        // Debug.Log(1 / Time.deltaTime);
    }

    public void AddBodyInteractive(float mass, float cameraDistance, Vector3 velocity, Vector3 angularVelocity)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log(mousePosition); //make it spawn in front of camera instead
        mousePosition.z += cameraDistance;


        AddBody(Mathf.Pow(10, mass), mousePosition, velocity, angularVelocity);


    }

    public GameObject AddBody(float mass, Vector3 position, Vector3 velocity)
    {
        return AddBody(mass, position, velocity, Vector3.zero);
    }

    public GameObject AddBody(float mass, Vector3 position, Vector3 velocity, Vector3 angularVelocity)
    {
        GameObject gameObject = Instantiate(bodyPrefab, position, Quaternion.identity);
        Body body = gameObject.GetComponent<Body>();
        body.mass = mass;
        body.position = position;
        body.velocity = velocity;
        body.transform.position = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        return gameObject;
    }
}

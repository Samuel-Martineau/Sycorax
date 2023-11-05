using UnityEngine;

public class Body : MonoBehaviour
{
    public float mass = 100; // kg
    public Vector3 velocity; // m / s
    public Vector3 position; // m
    public Vector3 angularVelocity; // rad / s
    public PlanetList planetList;
    public string planetName = "Default";
    public GameObject UIDisplay;

    public float Radius
    {
        get { return Mathf.Pow(3 * mass / 4 / Mathf.PI / Constants.ρ, 1f / 3f); } // m
    }

    public Vector3 Momentum
    {
        get { return velocity * mass; } // kg m / s
    }

    public Vector3 AngularMomentum
    {
        get { return 2 / 5 * mass * Mathf.Pow(Radius, 2) * angularVelocity; } // kg m² rad / s
    }
    void Start()
    {
        planetList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlanetList>();
        int i = Random.Range(0, planetList.nameList.Count);
        planetName = planetList.nameList[i];
        planetList.nameList.Remove(planetList.nameList[i]);

        UIDisplay = GameObject.FindGameObjectWithTag("PlanetInfo");
    }

    void OnTriggerEnter(Collider other)
    {
        Body otherBody = other.gameObject.GetComponent<Body>();
        if (GetInstanceID() < other.GetInstanceID()) return;
        Destroy(other.gameObject);
        Vector3 totalMomentum = Momentum + otherBody.Momentum;
        Vector3 totalAngularMomentum = AngularMomentum + otherBody.AngularMomentum;
        float totalMass = mass + otherBody.mass;
        velocity = totalMomentum / totalMass;
        angularVelocity = 5 / 2 * totalAngularMomentum / Mathf.Pow(Radius, 2) / totalMass;
        position = (mass * position + otherBody.mass * otherBody.position) / totalMass;
        mass = totalMass;
        Start();
        DisplayInfo();
    }

    void Update()
    {
        Quaternion Δrotation = Quaternion.Euler(angularVelocity * Time.deltaTime);
        transform.rotation = Δrotation * transform.rotation;
        transform.position = position / Constants.κ;
        float scale = Radius / Constants.λ;
        transform.localScale = new(scale, scale, scale);
        // TODO: Set scale of collider
    }

    public void DisplayInfo()
    {
        //UIDisplay.SetActive(!UIDisplay.activeSelf);
        // UIDisplay.GetComponent<PlanetInfo>().currentPlanet = gameObject;
    }
}

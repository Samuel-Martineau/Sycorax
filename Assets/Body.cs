using UnityEngine;

public class Body : MonoBehaviour
{
    public float mass = 100;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public PlanetList PList;
    public string planetName = "Default";
    public GameObject UIDisplay;

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

        PList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlanetList>();

        int i = Random.Range(0, PList.nameList.Count);
        planetName = PList.nameList[i];
        PList.nameList.Remove(PList.nameList[i]);

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
        angularVelocity = 5 / 2 * totalAngularMomentum / Mathf.Pow(radius, 2) / totalMass;
        transform.position = (mass * transform.position + otherBody.mass * otherBody.transform.position) / totalMass;
        mass = totalMass;
        Start();
    }

    void Update()
    {
        Quaternion Δrotation = Quaternion.Euler(angularVelocity * Time.deltaTime);
        transform.rotation = Δrotation * transform.rotation;
    }

    public void DisplayInfo()
    {
        //UIDisplay.SetActive(!UIDisplay.activeSelf);
        UIDisplay.GetComponent<PlanetInfo>().currentPlanet = gameObject;
    }
}

using System.Linq;
using Unity.VisualScripting;
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

    GameManager gameManager;


    public float Radius
    {
        get { return Mathf.Pow(3 * mass / 4 / Mathf.PI / gameManager.constantρ, 1f / 3f); } // m
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
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        if (position.magnitude == 0) position = transform.position * gameManager.constantκ;

        planetList = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlanetList>();
        int i = Random.Range(0, planetList.nameList.Count);
        planetName = planetList.nameList[i];
        planetList.nameList.Remove(planetList.nameList[i]);
        UIDisplay = GameObject.FindGameObjectWithTag("PlanetInfo");
    }

    void OnTriggerEnter(Collider other)
    {
        return;

        Body otherBody = other.gameObject.GetComponent<Body>();

        if (gameObject.GetInstanceID() < other.gameObject.GetInstanceID()) return;

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
        float scale = Mathf.Pow(Mathf.Log10(mass), 2) / gameManager.constantλ;
        transform.localScale = new(scale, scale, scale);
        transform.position = position / gameManager.constantκ;
        // transform.position = Utilities.arrayToVector3(Utilities.vector3ToArray(position).Select(x => Mathf.Sign(x) * Mathf.Log(Mathf.Abs(x) + 1) /* * Mathf.Pow(1 + Mathf.Pow(10, -Mathf.Abs(x) + gameManager.constantμ), -1) */).ToArray()) / gameManager.constantκ;
        // transform.position -= position.magnitude <= 1 ? Vector3.zero : (transform.position / transform.position.magnitude * gameManager.constantμ);
        // TODO: Set scale of collider
    }

    public void DisplayInfo()
    {
        //UIDisplay.SetActive(!UIDisplay.activeSelf);
        // UIDisplay.GetComponent<PlanetInfo>().currentPlanet = gameObject;
    }

    void MassSlider()
    {

    }

    public void SetValues(float mass1, float distanceFromCamera1, Vector3 initialVelocity1, Vector3 angularVelocity1){
        mass = mass1;
        velocity = initialVelocity1;
        angularVelocity = angularVelocity1;

        //integrate distance from camera

    }

}

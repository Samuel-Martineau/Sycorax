using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text NameText;



    public GameObject currentPlanet;
    public GameObject PlanetName;

    public GameObject MassSlider;

    Body Info;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TMP_text PlanetNameText = PlanetName.GetComponent<TMP_Text>();

        Info = currentPlanet.GetComponent<Body>();
        NameText.text = Info.planetName;


    }
}

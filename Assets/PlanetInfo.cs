using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlanetInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text NameText;



    public GameObject currentPlanet;
    public GameObject PlanetName;

    public GameObject MassSlider;

    public Slider slider;

    public TMP_Text massText;

    Body Info;
    void Start()
    {
        slider.onValueChanged.AddListener((value) => {
            currentPlanet.GetComponent<Body>().mass = value;
        });
    }

    // Update is called once per frame
    void Update()
    {
        //TMP_text PlanetNameText = PlanetName.GetComponent<TMP_Text>();
        if (currentPlanet){
             Info = currentPlanet.GetComponent<Body>();
            NameText.text = Info.planetName;

            massText.text = $"Mass: {Info.mass}";
        }
        else{
            NameText.text = "";
            
        }
       



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.PostProcessing;



public class SettingsUI : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    UIDocument document;
    Button closeButton;

    Slider constantG;
    Slider constantρ;
    Slider constantκ;
    Slider constantλ;
    Slider constantζ;
    Slider constantε;
    RadioButtonGroup gameMode;
    Toggle realScale;

    public GameObject MainUI;

    public GameObject EventSystem;
    public MoveCamera moveCamera;


    public PostProcessVolume ppVolume;
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        document = GetComponent<UIDocument>();

        closeButton = document.rootVisualElement.Q<Button>("CloseButton");
        constantG = document.rootVisualElement.Q<Slider>("GSlider");
        constantρ = document.rootVisualElement.Q<Slider>("DensitySlider");
        constantκ = document.rootVisualElement.Q<Slider>("LengthScaleSlider");
        constantλ = document.rootVisualElement.Q<Slider>("SizeScaleSlider");
        constantζ = document.rootVisualElement.Q<Slider>("TimeScaleSlider");
        constantε = document.rootVisualElement.Q<Slider>("TimePrecisionSlider");
        gameMode = document.rootVisualElement.Q<RadioButtonGroup>("Modes");
        realScale = document.rootVisualElement.Q<Toggle>("RealScale");

        closeButton.RegisterCallback<ClickEvent>(OnCloseClick);

        moveCamera = EventSystem.GetComponent<MoveCamera>();

        constantG.value = Mathf.Log10(gameManager.constantG);
        constantρ.value = Mathf.Log10(gameManager.constantρ);
        constantκ.value = Mathf.Log10(gameManager.constantκ);
        constantλ.value = Mathf.Log10(gameManager.constantλ);
        constantζ.value = Mathf.Log10(gameManager.constantζ);
        constantε.value = Mathf.Log10(gameManager.constantε);
        realScale.value = !gameManager.logScale;
        gameMode.value = (int)gameManager.gameMode;
    }

    void OnCloseClick(ClickEvent e)
    {

        gameManager.constantG = Mathf.Pow(10, constantG.value);
        gameManager.constantρ = Mathf.Pow(10, constantρ.value);
        gameManager.constantκ = Mathf.Pow(10, constantκ.value);
        gameManager.constantλ = Mathf.Pow(10, constantλ.value);
        gameManager.constantζ = Mathf.Pow(10, constantζ.value);
        gameManager.constantε = Mathf.Pow(10, constantε.value);
        gameManager.logScale = true;

        if (realScale.value)
        {
            gameManager.constantG = 6.67408e-11f;
            gameManager.constantρ = 1f;
            gameManager.constantλ = gameManager.constantκ;
            gameManager.logScale = false;
        }

        if (gameManager.gameMode != (GameMode)gameMode.value)
        {
            gameManager.gameMode = (GameMode)gameMode.value;
            IEnumerable<Body> bodies = FindObjectsOfType<Body>();
            foreach (Body body in bodies)
            {
                Destroy(body.gameObject);
            }
            if (gameManager.gameMode == GameMode.SolarSystem) gameManager.test();
        }


        this.gameObject.SetActive(false);
        ppVolume.enabled = false;
        MainUI.SetActive(true);
        moveCamera.moving = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

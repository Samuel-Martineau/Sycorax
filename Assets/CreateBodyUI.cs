using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.PostProcessing;

public class CreateBodyUI : MonoBehaviour
{
    // Start is called before the first frame update

    UIDocument buttonDocument;

    Button addBody;
    Button close;

    public GameObject MainUI;

    public GameManager gameManager;

    public GameObject EventSystem;
    MoveCamera moveCamera;

    public PostProcessVolume ppVolume;

    Slider massSlider;
    Slider cameraDistanceSlider;

    Vector3Field velocityField;

    Vector3Field angularVelocityField;


    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        addBody = buttonDocument.rootVisualElement.Q("AddButton") as Button;
        close = buttonDocument.rootVisualElement.Q("CloseButton") as Button;

        massSlider = buttonDocument.rootVisualElement.Q("MassSlider") as Slider; //name?
        cameraDistanceSlider = buttonDocument.rootVisualElement.Q("DistanceFromCameraSlider") as Slider;
        velocityField = buttonDocument.rootVisualElement.Q("InitialVelocity") as Vector3Field;
        angularVelocityField = buttonDocument.rootVisualElement.Q("AngularVelocity") as Vector3Field;

        addBody.RegisterCallback<ClickEvent>(OnCreateClick);
        close.RegisterCallback<ClickEvent>(OnCloseClick);
        moveCamera = EventSystem.GetComponent<MoveCamera>();
    }

    public void OnCreateClick(ClickEvent e)
    {

        Debug.Log($"{massSlider.value}");


        float mass = massSlider.value;
        Vector3 velocity = velocityField.value;
        Vector3 angularVelocity = angularVelocityField.value;
        float cameraDistance = cameraDistanceSlider.value;

        gameManager.AddBodyInteractive(mass, cameraDistance, velocity, angularVelocity);




        ppVolume.enabled = false;
        MainUI.SetActive(true);
        moveCamera.moving = true;
        this.gameObject.SetActive(false);
    }

    public void OnCloseClick(ClickEvent e)
    {
        Debug.Log("close clicked");

        ppVolume.enabled = false;
        MainUI.SetActive(true);
        moveCamera.moving = true;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame

}

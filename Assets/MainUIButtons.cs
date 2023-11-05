using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.PostProcessing;

public class MainUIButtons : MonoBehaviour
{
    // Start is called before the first frame update

    UIDocument buttonDocument;

    Button settingsButton;
    Button createObject;

    public GameObject EventSystem;

    
    public PostProcessVolume ppVolume;
    MoveCamera moveCamera;

    public GameObject settingsUI;

    public GameObject createBodyUI;
    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        settingsButton = buttonDocument.rootVisualElement.Q("Settings") as Button;
        createObject = buttonDocument.rootVisualElement.Q("CreateBody") as Button;

        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);

        createObject.RegisterCallback<ClickEvent>(OnCreateClick);
        moveCamera=EventSystem.GetComponent<MoveCamera>();

        //ppVolume = EventSystem.GetComponent<PostProcessingVolume>();


        
    }
    public void OnSettingsClick(ClickEvent e){
        //Debug.Log("Settings button clicked");

        ppVolume.enabled = true;
        settingsUI.SetActive(true);
        this.gameObject.SetActive(false);
        moveCamera.moving = false;
        
        //Pause game


    }

    public void OnCreateClick(ClickEvent e){
        Debug.Log("Create button clicked");

        createBodyUI.SetActive(true); //AddButton CloseButton




        //Pause Game
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

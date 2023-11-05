using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering.PostProcessing;



public class SettingsUI : MonoBehaviour
{
    // Start is called before the first frame update
    UIDocument buttonDocument;
    Button CloseButton;
    public GameObject MainUI;

    public GameObject EventSystem;
    public MoveCamera moveCamera;


    public PostProcessVolume ppVolume;
    private void OnEnable() {
        buttonDocument = GetComponent<UIDocument>();

        CloseButton = buttonDocument.rootVisualElement.Q("CloseButton") as Button;

        CloseButton.RegisterCallback<ClickEvent>(OnCloseClick);

        moveCamera = EventSystem.GetComponent<MoveCamera>();
    }

    void OnCloseClick(ClickEvent e){

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

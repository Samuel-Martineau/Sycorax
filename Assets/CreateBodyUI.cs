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
    public GameObject ProjectFunctions;

    public GameManager gameManager;

    public GameObject EventSystem;
    MoveCamera moveCamera;
    public GameObject settingsUI;

    public PostProcessVolume ppVolume;
    
    void OnEnable()
    {
        buttonDocument = GetComponent<UIDocument>();

        addBody = buttonDocument.rootVisualElement.Q("CreateButton") as Button;
        close = buttonDocument.rootVisualElement.Q("CreateButton") as Button;

        addBody.RegisterCallback<ClickEvent>(OnCreateClick);

        close.RegisterCallback<ClickEvent>(OnCloseClick);
        moveCamera=EventSystem.GetComponent<MoveCamera>();


    }

    public void OnCreateClick(ClickEvent e){

        gameManager.AddBody();


        this.gameObject.SetActive(false);
        ppVolume.enabled = false;
        MainUI.SetActive(true);
        moveCamera.moving = true;
    }

    public void OnCloseClick(ClickEvent e){
        this.gameObject.SetActive(false);
        ppVolume.enabled = false;
        MainUI.SetActive(true);
        moveCamera.moving = true;
    }

    // Update is called once per frame
    
}

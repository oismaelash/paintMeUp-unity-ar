using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
    #region VARIABLES

    public static AppController Instance;
    [SerializeField] private ColorPicker colorPicker;
    [SerializeField] private int timeMax;
    [SerializeField] private List<Color> colorFindList;
    [SerializeField] private Image colorPreviewImage;
    [SerializeField] private RawImage screenshotRawImage;
    [SerializeField] private Button getColorButton;
    [SerializeField] private GameObject canvasGameplay;

    public PersonSO PersonSOSelected;
    public int colorsCountFindCurrent;
    public int ColorsCountFindCurrent
    {
        get { return colorsCountFindCurrent; }
        set
        {
            colorsCountFindCurrent = value;

            if(PersonSOSelected != null && colorsCountFindCurrent == PersonSOSelected.PersonModel.GetColors().Count)
            {
                Debug.Log("Finish game");
                SceneManager.LoadScene(0);
            }
        }
    }

    public Action<Renderer> partOfBodyChooseEvent;
    public Action<Color> colorChooseEvent;

    [SerializeField] private Renderer chooseRenderer;


    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Start()
    {
        Instance = this;
        ColorsCountFindCurrent = 0;
        getColorButton.onClick.AddListener(OnButtonGetColorClicked);
        colorPicker.OnColorPickerEvent += (color) =>
        {
            chooseRenderer.material.color = color;

            //PersonModel personModel = FindObjectOfType<PersonModel>();

            //if(personModel is null)
            //{
            //    Debug.LogError("personModel is null");
            //    return;
            //}

            //if (personModel.GetPartOfBodyDict().ContainsKey(ColorUtility.ToHtmlStringRGBA(color)))
            //{
            //    personModel.GetPartOfBodyDict()[ColorUtility.ToHtmlStringRGBA(color)].material.color = color;
            //    ColorsCountFindCurrent++;
            //}
        };

        partOfBodyChooseEvent += PartOfBodyChooseEvent;
    }

    #endregion

    #region PRIVATE_METHODS

    private void OnButtonGetColorClicked()
    {
        //renderTexture.width = Screen.width;
        //renderTexture.height = Screen.height;
        //Camera.main.targetTexture = renderTexture;
        StartCoroutine(GetScreenshot_Coroutine());
    }

    private void PartOfBodyChooseEvent(Renderer renderer)
    {
        chooseRenderer = renderer;
        renderer.material.color = Color.gray;
    }

    #endregion

    #region COROUTINES

    private IEnumerator GetScreenshot_Coroutine()
    {
        canvasGameplay.SetActive(false);
        yield return new WaitForSeconds(.2f);
        Texture2D screenshotTexture2D = ScreenCapture.CaptureScreenshotAsTexture();
        Debug.Log($"screenshotTexture2D.isReadable: {screenshotTexture2D.isReadable}");
        yield return new WaitForSeconds(.2f);
        canvasGameplay.SetActive(true);
        screenshotRawImage.gameObject.SetActive(true);
        screenshotRawImage.texture = null;
        screenshotRawImage.texture = screenshotTexture2D;
    }

    #endregion
}
    
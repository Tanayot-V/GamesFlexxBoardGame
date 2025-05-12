using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BriefMe;

namespace PersonalValue
{       

public class CropImage : MonoBehaviour
{
    public GameObject cropObj;
    public Image[] imgCropAll;
    public Image imgCrop;
    private RectTransform rectTransform;
    public Slider zoomSlider; 
    public Image priorityLastIMG; 

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.01f;
    private float minScale = 0.15f;
    private float maxScale = 3f;

     [Header("Rotat Settings")]
     private int[] rotationAngles = { 0, 90, 180, 270 };
    private int currentRotatIndex = 0;
    public bool isUploadIMG;

    private void Start()
    {
        rectTransform = imgCrop.GetComponent<RectTransform>();
        // ✅ Set Slider Value
        zoomSlider.minValue = minScale;
        zoomSlider.maxValue = maxScale;
        zoomSlider.value = imgCrop.GetComponent<RectTransform>().localScale.x;

        // ✅ Set Slider Event
        zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
    }
    public void OnZoomSliderChanged(float value)
    {
        rectTransform.localScale = new Vector3(value, value, 1);
        imgCropAll.ToList().ForEach(x => x.rectTransform.localScale = imgCrop.rectTransform.localScale);
    }

    void Update()
    {
        if(cropObj.activeInHierarchy)
        {
            imgCropAll.ToList().ForEach(x => x.sprite = imgCrop.sprite);
            imgCropAll.ToList().ForEach(x => x.rectTransform.localScale = imgCrop.rectTransform.localScale);
            imgCropAll.ToList().ForEach(x => x.rectTransform.position = imgCrop.rectTransform.position);
            imgCropAll.ToList().ForEach(x => x.rectTransform.rotation = imgCrop.rectTransform.rotation);
        }
    }

    private void Zoom(float delta)
    {
        float scale = rectTransform.localScale.x;
        scale += delta * zoomSpeed;
        scale = Mathf.Clamp(scale, minScale, maxScale);

        rectTransform.localScale = new Vector3(scale, scale, 1);
    }

    public void SetCropImagePosition()
    {
        imgCropAll.ToList().ForEach(x => x.rectTransform.localPosition = imgCrop.rectTransform.localPosition);
    }

    public void SetSizeAllImage()
    {
        imgCropAll.ToList().ForEach(x => x.rectTransform.sizeDelta = imgCrop.rectTransform.sizeDelta);
        isUploadIMG = true;
    }

    public void RotateNextButton()
    {
        currentRotatIndex = (currentRotatIndex + 1) % rotationAngles.Length;
        imgCrop.transform.DORotate(new Vector3(0, 0, rotationAngles[currentRotatIndex]), 0.5f).SetEase(Ease.OutQuad);
    }

    public void UploadImgButton()
    {
        if(imgCrop.GetComponent<Image>().sprite == null)
        {
            //GameManager.Instance.webGLFileLoaderButton.OnFileSelected(string.Empty);
            //GameManager.Instance.webGLFileLoaderButton.PickImage();
            OpenCropSetting();
            return;
        }
        else
        {
           OpenCropSetting();
        }

        void OpenCropSetting()
        {
            //GameManager.Instance.webGLFileLoaderButton.ShowFileInputButton();
            GameManager.Instance.levelManager.cropImagePage.SetActive(true);
        }
    }

    public void CloseCropPage()
    {
        GameManager.Instance.levelManager.cropImagePage.SetActive(false);
        GameManager.Instance.webGLFileLoaderButton.HideFileInputButton();
        priorityLastIMG.sprite = imgCropAll[0].sprite;
        
        RectTransform src = imgCropAll[0].rectTransform;
        RectTransform dest = priorityLastIMG.GetComponent<RectTransform>();

        // คัดลอกค่า Transform ทั้งหมด
        dest.anchoredPosition = src.anchoredPosition;
        dest.sizeDelta = src.sizeDelta;
        dest.anchorMin = src.anchorMin;
        dest.anchorMax = src.anchorMax;
        dest.pivot = src.pivot;
        dest.localRotation = src.localRotation;
        dest.localScale = src.localScale;
        dest.localPosition = src.localPosition;
    }
}
}

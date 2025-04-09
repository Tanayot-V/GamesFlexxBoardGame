using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CropImage : MonoBehaviour
{
    public Image[] imgCropAll;
    public Image imgCrop;
    private RectTransform rectTransform;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.01f;
    public float minScale = 0.5f;
    public float maxScale = 3f;

    private void Awake()
    {
        rectTransform = imgCrop.GetComponent<RectTransform>();
    }

    void Update()
    {
        // ✅ Mouse Scroll Zoom (PC)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Zoom(scroll * 10); // เพิ่ม sensitivity สำหรับ scroll
        }

        // ✅ Touch Pinch Zoom (Mobile)
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // คำนวณระยะห่างระหว่างนิ้วก่อนและหลัง
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevDistance = Vector2.Distance(prevTouch0, prevTouch1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);
            float delta = currentDistance - prevDistance;

            Zoom(delta);
        }
    }

    private void Zoom(float delta)
    {
        float scale = rectTransform.localScale.x;
        scale += delta * zoomSpeed;
        scale = Mathf.Clamp(scale, minScale, maxScale);

        rectTransform.localScale = new Vector3(scale, scale, 1);
    }
}

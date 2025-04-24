using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using PersonalValue;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WebGLFileLoaderButton : MonoBehaviour
{
    public Image targetImage;

    // Import ฟังก์ชัน JavaScript
    [DllImport("__Internal")]
    private static extern void InitializeVisibleFileInput();
    
    [DllImport("__Internal")]
    private static extern void ShowFileInput();

    [DllImport("__Internal")]
    private static extern void HideFileInput();

    private bool isShowFileInputButton;
    
    void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            // เรียกใช้เมื่อเกมเริ่มทำงาน
            InitializeVisibleFileInput();

            // ซ่อนไว้ก่อนตอนเริ่มต้น
            HideFileInput();

        #endif
    }

    public void PickImage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        //InitializeVisibleFileInput();
        if(!isShowFileInputButton)  
        {
            ShowFileInputButton(); // ✅ เปิด File Picker ผ่าน JavaScript
        }
        else 
        {
            HideFileInputButton();
        }
#else
        PickImageInEditor();
        Debug.Log("File Picker works only in WebGL.");
#endif
    }

    private void PickImageInEditor()
    {
#if UNITY_EDITOR
        string filePath = EditorUtility.OpenFilePanel("เลือกไฟล์รูป", "", "png,jpg,jpeg");

        if (!string.IsNullOrEmpty(filePath))
        {
            byte[] imageBytes = File.ReadAllBytes(filePath);
            string base64Image = System.Convert.ToBase64String(imageBytes);
            StartCoroutine(LoadImageFromBase64(base64Image));
        }
#else
        Debug.LogError("File Picker ใช้งานได้เฉพาะใน Unity Editor เท่านั้น!");
#endif
    }
    
    public void OnFileSelected(string base64Image)
    {
        //Debug.Log("✅ Image received: " + base64Image.Substring(0, 50) + "..."); 
        StartCoroutine(LoadImageFromBase64(base64Image));
    }

    // เมธอดที่จะถูกเรียกจาก JavaScript
    public IEnumerator LoadImageFromBase64(string base64Data)
    {
       Debug.Log("⏳ Decoding Base64...");

        byte[] imageBytes = System.Convert.FromBase64String(base64Data);
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        texture.LoadImage(imageBytes);

        if (texture.width == 2 && texture.height == 2)
        {
            Debug.LogError("❌ Failed to load image. Texture size is invalid!");
            yield break;
        }

        Debug.Log("✅ Image decoded successfully. Applying to UI...");

        targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        targetImage.preserveAspect = true;
        
        ColliderSetting(texture);
        /*
        // ซ่อน input เมื่อเลือกไฟล์เสร็จแล้ว
        #if UNITY_WEBGL && !UNITY_EDITOR
            HideFileInput();
        #endif
        */

        yield return null;
    }

     private void ColliderSetting(Texture2D texture)
    {   
        RectTransform rect = targetImage.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(texture.width, texture.height);

        if(texture.width < 600) rect.localScale = new Vector3(1f, 1f, 1);
        else if(texture.width > 1200)rect.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if(texture.width > 2400)rect.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        else rect.localScale = new Vector3(1f, 1f, 1);

        GameManager.Instance.cropImage.zoomSlider.value = rect.localScale.x;

         BoxCollider2D box = targetImage.GetComponent<BoxCollider2D>();
        if (box != null)
        {
            float width = texture.width;   // แปลงจาก px → world space
            float height = texture.height;
            box.size = new Vector2(width, height);
        }

        GameManager.Instance.cropImage.SetSizeAllImage();
        Debug.Log($"✅ Resize to {texture.width} x {texture.height} px สำเร็จ");
    }


    public void ShowFileInputButton()
    {
         #if UNITY_WEBGL && !UNITY_EDITOR
            ShowFileInput();
            isShowFileInputButton = true;
        #endif
    }

    public void HideFileInputButton()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
            HideFileInput();
            isShowFileInputButton = false;
        #endif
    }
}

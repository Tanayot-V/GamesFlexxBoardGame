using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WebGLFileLoader : MonoBehaviour
{ 
    [DllImport("__Internal")]
    private static extern void OpenFilePicker();
    public Image targetImage;
    public void Start()
    {
        OnFileSelected(string.Empty);
    }
     public void PickImage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        OpenFilePicker(); // ✅ เปิด File Picker ผ่าน JavaScript
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

    private IEnumerator LoadImageFromBase64(string base64)
    {
        Debug.Log("⏳ Decoding Base64...");

        byte[] imageBytes = System.Convert.FromBase64String(base64);
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
        yield return null;
    }
}

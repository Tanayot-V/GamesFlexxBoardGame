using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Linq;


public class WebGLShare : MonoBehaviour
{
    public GameObject[] saveButton;

    [DllImport("__Internal")]
    private static extern void ShareImage(string base64Image);
    [DllImport("__Internal")]
    private static extern void ShareText(string text);

   public void ShareText()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShareText("🎮 มาลองเล่นเกมนี้! 👉 https://yourgame.com");
#else
        Debug.Log("📌 Running in Editor: Simulating share...");
#endif
    }

    public void ShareImage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(CaptureScreenshotAndShare());
#else
        Debug.Log("📌 Running in Editor: Simulating image share...");
#endif
    }

    //จะทดสอบแชร์ต้อง build ขึ้น Https//: เท่านั้น ทดสอบแชร์ local ไม่ได้
    IEnumerator CaptureScreenshotAndShare()
    {
        saveButton.ToList().ForEach(o => { o.SetActive(false); });

        yield return new WaitForEndOfFrame();
        
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        byte[] imageData = screenshot.EncodeToPNG();
        string base64Image = System.Convert.ToBase64String(imageData);
        string base64String = "data:image/png;base64," + base64Image;

        saveButton.ToList().ForEach(o => { o.SetActive(false); });
        Destroy(screenshot);

        // 📤 ส่งไปที่ Web Share API
        ShareImage(base64String);
    }
}

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

    [DllImport("__Internal")]
    private static extern void ShareOptimizedForFacebook(string base64Image, string titlePtr,string textPtr, string urlPtr);

   public void ShareText()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShareText("🎮 มาลองเล่นเกมนี้!");
#else
        Debug.Log("📌 Running in Editor: Simulating share...");
#endif
    }

    public void ShareImage()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(CaptureScreenshotAndShare());
        //ShareScreenshotWithLink("มาเล่นเกมนี้!", "ลองเล่นเกมนี้! 👉 https://gamesflexx.github.io/BoardGame/Games/PersonalValue", "https://gamesflexx.github.io/BoardGame/Games/PersonalValue");
#else
        Debug.Log("📌 Running in Editor: Simulating image share...");
#endif
    }

    public void ShareImageWithLink()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        StartCoroutine(CaptureScreenshotAndSharee());
#else
        Debug.Log("📌 Running in Editor: Simulating image and link share...");
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

        saveButton.ToList().ForEach(o => { o.SetActive(true); });
        Destroy(screenshot);

        // 📤 ส่งไปที่ Web Share API
        ShareImage(base64String);
    }

    private IEnumerator CaptureScreenshotAndSharee()
    {
         // รอให้เฟรมเรนเดอร์เสร็จสมบูรณ์
        yield return new WaitForEndOfFrame();

        // จับภาพหน้าจอ
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // แปลงเป็น Base64 พร้อม header
        string base64Image = "data:image/png;base64," + Convert.ToBase64String(screenshot.EncodeToPNG());
        Destroy(screenshot);

        // เรียกใช้ฟังก์ชัน JavaScript
        ShareOptimizedForFacebook(base64Image,"title","มาเล่นเกมนี้","https://gamesflexx.github.io/BoardGame/Games/PersonalValue");
    }
}

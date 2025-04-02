using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class WebGLScreenshot : MonoBehaviour
{
   [DllImport("__Internal")]
    private static extern void DownloadScreenshot(string base64Image);
    [DllImport("__Internal")]
    private static extern void ShareBase64Image(string base64Image);
    public void CaptureScreenshot()
    {
       StartCoroutine(TakeScreenshot());
      
    }
    public void ShareScreenshot()
    {
          StartCoroutine(TakeScreenshotShare());
    }


    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        // สร้าง Texture2D สำหรับแคปหน้าจอ
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // แปลงเป็น PNG แล้วเก็บเป็นไบต์อาร์เรย์
        byte[] imageData = screenshot.EncodeToPNG();

        // แปลงเป็น Base64 (WebGL ไม่รองรับไฟล์โดยตรง ต้องใช้ Base64)
        string base64Image = System.Convert.ToBase64String(imageData);

        // เรียกใช้ JavaScript เพื่อนำรูปไปใช้งานหรือดาวน์โหลด
          // ตรวจสอบให้ทำงานเฉพาะ WebGL
        #if UNITY_WEBGL && !UNITY_EDITOR
        DownloadScreenshot(base64Image);
        #else
        Debug.Log("WebGL function only works in WebGL build!");
        #endif

        // ลบ Texture เพื่อลดการใช้หน่วยความจำ
        Destroy(screenshot);
    }

    private IEnumerator TakeScreenshotShare()
    {
        yield return new WaitForEndOfFrame();

        // สร้าง Texture2D สำหรับแคปหน้าจอ
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // แปลงเป็น Base64
        byte[] imageData = screenshot.EncodeToPNG();
        string base64Image = System.Convert.ToBase64String(imageData);

        // ส่งไป JavaScript เพื่อแชร์
        #if UNITY_WEBGL && !UNITY_EDITOR
        ShareBase64Image(base64Image);
        #else
        Debug.Log("ShareBase64Image only works in WebGL build!");
        #endif
        // ลบ Texture เพื่อลดการใช้หน่วยความจำ

        Destroy(screenshot);
    }
}

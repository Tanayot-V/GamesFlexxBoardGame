using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class FileIOUploader : MonoBehaviour
{
       private string fileIOUrl = "https://file.io/?expires=1d"; // ✅ ตั้งให้ไฟล์หมดอายุใน 1 วัน

    public void UploadScreenshotAndShare()
    {
        StartCoroutine(CaptureAndUpload());
    }

    private IEnumerator CaptureAndUpload()
    {
        yield return new WaitForEndOfFrame();

        // ✅ 1. แคปหน้าจอเป็น Texture2D
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        byte[] imageData = screenshot.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageData, "screenshot.png", "image/png"); // ✅ ใช้ multipart/form-data

        // ✅ 2. อัปโหลดไฟล์โดยใช้ `multipart/form-data`
        using (UnityWebRequest www = UnityWebRequest.Post(fileIOUrl, form))
        {
            www.SetRequestHeader("Accept", "application/json"); // ✅ ขอให้ API ส่ง JSON Response กลับมา
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // ✅ 3. ดึง URL ของไฟล์ที่อัปโหลดสำเร็จ
                string responseText = www.downloadHandler.text;
                string fileUrl = ExtractFileUrl(responseText);

                if (!string.IsNullOrEmpty(fileUrl))
                {
                    Debug.Log("Uploaded to File.io: " + fileUrl);
                    
                    // ✅ 4. แชร์ไป Facebook
                    ShareToFacebook(fileUrl);
                }
                else
                {
                    Debug.LogError("Failed to extract File.io URL.");
                }
            }
            else
            {
                Debug.LogError("Upload failed: " + www.error);
            }
        }

        Destroy(screenshot);
    }

    private string ExtractFileUrl(string jsonResponse)
    {
        int startIndex = jsonResponse.IndexOf("\"link\":\"") + 8;
        if (startIndex == -1) return null;
        int endIndex = jsonResponse.IndexOf("\"", startIndex);
        return jsonResponse.Substring(startIndex, endIndex - startIndex);
    }

    private void ShareToFacebook(string imageUrl)
    {
        string fbUrl = "https://www.facebook.com/sharer/sharer.php?u=" + UnityWebRequest.EscapeURL(imageUrl);
        Application.OpenURL(fbUrl);
    }
}
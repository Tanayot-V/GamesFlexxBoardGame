using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Security.Cryptography;
using System.Text;

public class CloudinaryUploader : MonoBehaviour
{
    private string cloudName = "dlrv35jyt"; // ใส่ Cloud Name ที่ถูกต้อง
    private string apiKey = "628775117845917"; // ใส่ API Key จาก Cloudinary
    private string apiSecret = "utR7-iGq5GHxsTr3bHm2O2hYScI"; // ใส่ API Secret จาก Cloudinary
    private string uploadPreset = "Personal Value"; // ใช้สำหรับอัปโหลด

    public void UploadScreenshotAndShare()
    {
        StartCoroutine(CaptureAndUpload());
    }

    private IEnumerator CaptureAndUpload()
    {
        yield return new WaitForEndOfFrame();

        // แคปหน้าจอเป็น Texture2D
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        byte[] imageData = screenshot.EncodeToPNG();
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageData, "screenshot.png", "image/png");
        form.AddField("upload_preset", uploadPreset); // ใช้ Upload Preset

        using (UnityWebRequest www = UnityWebRequest.Post($"https://api.cloudinary.com/v1_1/{cloudName}/image/upload", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseText = www.downloadHandler.text;
                string imageUrl = ExtractImageUrl(responseText);
                string publicId = ExtractPublicId(responseText);
                string assetId = ExtractAssetId(responseText);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    Debug.Log("Uploaded to Cloudinary: " + imageUrl);
                    
                    // ✅ แชร์ไป Facebook
                    ShareToFacebook(imageUrl);
                    Debug.Log(responseText);
        Debug.Log("Public ID: " + publicId);
        Debug.Log("Asset ID: " + assetId);
        string[] assetIds = { assetId };
                    StartCoroutine(DeleteImagesAfterDelay(assetIds,3));  
                }
            }
            else
            {
                Debug.LogError("Upload failed: " + www.error);
            }
        }

        Destroy(screenshot);
    }

    private string ExtractImageUrl(string jsonResponse)
    {
        int startIndex = jsonResponse.IndexOf("\"secure_url\":\"") + 14;
        if (startIndex == -1) return null;
        int endIndex = jsonResponse.IndexOf("\"", startIndex);
        return jsonResponse.Substring(startIndex, endIndex - startIndex);
    }

   private string ExtractPublicId(string jsonResponse)
{
    int startIndex = jsonResponse.IndexOf("\"public_id\":\"") + 13;
    if (startIndex == -1) return null;
    int endIndex = jsonResponse.IndexOf("\"", startIndex);
    return jsonResponse.Substring(startIndex, endIndex - startIndex);
}

    private string ExtractAssetId(string jsonResponse)
    {
        int startIndex = jsonResponse.IndexOf("\"asset_id\":\"") + 12;
        if (startIndex == -1) return null;
        int endIndex = jsonResponse.IndexOf("\"", startIndex);
        return jsonResponse.Substring(startIndex, endIndex - startIndex);
    }

    private void ShareToFacebook(string imageUrl)
    {
        string fbUrl = "https://www.facebook.com/sharer/sharer.php?u=" + UnityWebRequest.EscapeURL(imageUrl);
        Application.OpenURL(fbUrl);
    }

    private IEnumerator DeleteImagesAfterDelay(string[] assetIds, float delay)
    {
       Debug.Log("⏳ Waiting " + delay + " seconds before deleting images...");

        // นับถอยหลังทีละ 1 วินาที
        while (delay > 0)
        {
            Debug.Log("⌛ Deleting in " + delay + " seconds...");
            yield return new WaitForSeconds(1f);
            delay--;
        }

        Debug.Log("✅ Now deleting images...");
        StartCoroutine(DeleteImageRequest(assetIds));
    }

    private IEnumerator DeleteImageRequest(string[] assetIds)
    {
        string url = $"https://api.cloudinary.com/v1_1/{cloudName}/resources/";

        // ✅ สร้าง JSON Body
        string jsonBody = "{\"asset_ids\": [";
        for (int i = 0; i < assetIds.Length; i++)
        {
            jsonBody += "\"" + assetIds[i] + "\"";
            if (i < assetIds.Length - 1) jsonBody += ", ";
        }
        jsonBody += "]}";

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest www = UnityWebRequest.Delete(url))
        {
            // ✅ ใส่ Body
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.SetRequestHeader("Content-Type", "application/json");

            // ✅ ใช้ Basic Authentication
            string auth = EncodeToBase64(apiKey + ":" + apiSecret);
            www.SetRequestHeader("Authorization", "Basic " + auth);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ Deleted images from Cloudinary");
            }
            else
            {
                Debug.LogError("❌ Failed to delete images: " + www.error);
            }
        }
    }
 private string EncodeToBase64(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return System.Convert.ToBase64String(bytes);
    }

    private string GenerateSignature(string input)
    {
        var encoding = new UTF8Encoding();
        byte[] keyBytes = encoding.GetBytes(apiSecret);
        byte[] messageBytes = encoding.GetBytes(input);

        using (var hmacsha1 = new HMACSHA1(keyBytes))
        {
            byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
            return System.BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
        }
    }
}
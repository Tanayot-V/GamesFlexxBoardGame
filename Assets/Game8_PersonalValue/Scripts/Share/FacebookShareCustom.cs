using UnityEngine;
using System.Collections;
using System.IO;

public class FacebookShareCustom : MonoBehaviour
{
    public string baseShareUrl = "https://gamesflexxhistory.web.app/GamesFlexxBoardGame/Games/Share3.html"; // URL หน้าเว็บแชร์

    public void ShareToFacebook(int score, string description, Texture2D image)
    {
        StartCoroutine(UploadAndShare(score, description, image));
    }

    private IEnumerator UploadAndShare(int score, string description, Texture2D image)
    {
        byte[] imageData = image.EncodeToPNG();
        string uploadUrl = "https://yourserver.com/upload.php"; // API สำหรับอัปโหลดรูป

        WWWForm form = new WWWForm();
        form.AddBinaryData("image", imageData, "share.png", "image/png");

        using (WWW www = new WWW(uploadUrl, form))
        {
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                string uploadedImageUrl = www.text; // ได้ URL รูปที่อัปโหลดสำเร็จ
                string encodedDesc = UnityEngine.Networking.UnityWebRequest.EscapeURL(description);
                string shareUrl = baseShareUrl + "?score=" + score + "&desc=" + encodedDesc + "&img=" + uploadedImageUrl;
                Application.OpenURL("https://www.facebook.com/sharer/sharer.php?u=" + shareUrl);
            }
            else
            {
                Debug.LogError("Error uploading image: " + www.error);
            }
        }
    }
}
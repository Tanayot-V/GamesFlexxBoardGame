using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facebook : MonoBehaviour
{
     //private string baseShareUrl = "https://gamesflexx.github.io/BoardGame/Games/DailyIntention/Share.html"; // URL ของหน้าแชร์
     private string baseShareUrl = "https://gamesflexxhistory.web.app/GamesFlexxBoardGame/Games/Share3.html"; // URL ของหน้าแชร์
     int score = 100; // คะแนนที่ได้
     

    public void ShareWithFacebook()
    {
        /*
        string description = "ฉันทำคะแนนได้ " + score + " คะแนน! มาท้าประลองกันเถอะ!"; // คำอธิบายที่จะแชร์
        // แปลงคำอธิบายเป็น URL Encoded
        string encodedDesc = UnityEngine.Networking.UnityWebRequest.EscapeURL(description);

        // เพิ่มค่าคะแนนและคำอธิบายลงใน URL
        string shareUrl = baseShareUrl + "?score=" + 100 ;//+ "&desc=" + "ก็มาเด้";
*/
        // เปิด Facebook Share Dialog
        string fbUrl = "https://www.facebook.com/sharer/sharer.php?u=" + baseShareUrl;
        Application.OpenURL(fbUrl);
    }
}

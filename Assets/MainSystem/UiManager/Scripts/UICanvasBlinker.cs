using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UICanvasBlinker : MonoBehaviour
{
    public CanvasGroup canvasGroup;     // ลาก CanvasGroup ที่ต้องการให้กระพริบมาใส่

    public float fadeDuration = 0.5f;   // ระยะเวลาของแต่ละ fade (in หรือ out)

    void Start()
    {
        StartBlink();
    }

    public void StartBlink()
    {
        // เริ่มที่มองเห็น (1), ค่อย ๆ fade ไป 0 และย้อนกลับ → loop
        canvasGroup.DOFade(0f, fadeDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine); // ลื่น ๆ นุ่ม ๆ
    }

    public void StopBlink()
    {
        // หยุด tween ทั้งหมดของ canvasGroup
        DOTween.Kill(canvasGroup);
        canvasGroup.alpha = 1f; // คืนค่าให้มองเห็นเต็ม
    }
}

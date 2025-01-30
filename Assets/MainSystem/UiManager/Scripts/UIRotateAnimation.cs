using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class UIRotateAnimation : MonoBehaviour
{
     public float rotationSpeed = 2f; // กำหนดเวลาให้หมุนครบ 1 รอบ (วินาที)
     
    void Start()
    {
        GetComponent<RectTransform>()
            .DORotate(new Vector3(0, 0, 360), rotationSpeed, RotateMode.FastBeyond360) // หมุนรอบตัวเอง
            .SetLoops(-1, LoopType.Restart) // หมุนซ้ำไปเรื่อย ๆ
            .SetEase(Ease.Linear); // หมุนด้วยความเร็วคงที่
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

namespace PersonalValue
{
    public class GridScrollController : MonoBehaviour
    {
         public RectTransform content;            // Content ของ ScrollView
        public int cardsPerRow = 5;              // จำนวนไพ่ต่อแถว
        public float rowHeight = 350f;           // ความสูงแต่ละแถว
        
        private int currentRow = 0;
        private int totalRows = 0;
        private bool isScrolling = false;


        private List<CardDataSO> cardItems = new List<CardDataSO>();       // ไพ่ทั้งหมด (สามารถอัปเดตภายนอกได้)

    public void RefreshGrid(List<CardDataSO> _cardItems)
    {
        cardItems = _cardItems;
        // คำนวณจำนวนแถว
        totalRows = Mathf.CeilToInt((float)cardItems.Count / cardsPerRow);
        currentRow = 0;

        // รีเซ็ตตำแหน่ง Content ไปที่แถวแรก
        SetContentPosition();
    }

    public void ScrollUp()
    {
        if (isScrolling || currentRow <= 0) return;

        currentRow--;
        SetContentPosition();
    }

    public void ScrollDown()
    {
       if (isScrolling || currentRow >= totalRows - 1) return;

        currentRow++;
        SetContentPosition();
    }

    private void SetContentPosition()
    {
        isScrolling = true;

    float targetY = rowHeight * currentRow;

        // Kill การ scroll ก่อนหน้า (ป้องกันการค้าง)
        DOTween.Kill(content);

        content.DOAnchorPosY(targetY, 0.4f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => isScrolling = false); // ปลดล็อกเมื่อ scroll เสร็จ

        Debug.Log($"📌 Scroll to row {currentRow + 1} / {totalRows} (Pos Y = {targetY})");
    }
    }
}

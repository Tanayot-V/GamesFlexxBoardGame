using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PersonalValue
{
    public class DragDropCropIMG : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Transform originalParent;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false; // ให้ลากผ่าน Raycast ได้
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.root.GetComponent<Canvas>().scaleFactor;
        //GameManager.Instance.cropImage.SetCropImagePosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        canvasGroup.blocksRaycasts = true;
        //GameManager.Instance.cropImage.SetCropImagePosition();

        /*
        // ถ้าไม่มีที่ Drop ให้กลับที่เดิม
        if (transform.parent == originalParent)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
        */
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"{name} is still overlapping with {other.name}");
    }

        void OnTriggerExit2D(Collider2D other)
        {
              if (other.CompareTag("Menu"))
                {
                    if(GameManager.Instance.levelManager.cropImagePage.activeInHierarchy)
                    {
                        // ถ้าออกจากพื้นที่ Drop ให้กลับที่เดิม
                        rectTransform.anchoredPosition = Vector2.zero;
                        transform.SetParent(originalParent);
                        canvasGroup.blocksRaycasts = true; // ปิดการลาก
                    }
                }
        }
    }
}

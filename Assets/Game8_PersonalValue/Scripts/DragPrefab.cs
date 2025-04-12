using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace PersonalValue
{
    public class DragPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector3 offset;
        public GameObject dropBox;
        public DragDropCard dragDropCard;
        public Color dragColor;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        canvasGroup.blocksRaycasts = false;
        offset = rectTransform.position - Camera.main.ScreenToWorldPoint(eventData.position);
        dropBox = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(eventData.position) + offset;
        rectTransform.position = new Vector3(newPosition.x, newPosition.y, rectTransform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
      
    }

    public void SetCardToBox()
    {
        LevelManager levelManager = GameManager.Instance.levelManager;
       if(dropBox != null)
       {
        if(levelManager.currentStage == Stage.Stage4)
        {
            if( dropBox.GetComponent<DropBox>().cardName_Stage4 != null)
            {
              levelManager.CreateCard(levelManager.cardPrefab,levelManager.priorityParent,dropBox.GetComponent<DropBox>().cardName_Stage4);
            }
            dropBox.GetComponent<DropBox>().cardName_Stage4 = dragDropCard.cardDataSO;

            dropBox.GetComponent<DropBox>().transform.GetChild(0).GetComponent<Image>().sprite = dragDropCard.cardDataSO.picture;
            levelManager.UpdateFillCount_Stage4();
            Destroy(dragDropCard.gameObject);
            return;
        }
        else
        {
            dropBox.GetComponent<DropBox>().cardDataSOList.Add(dragDropCard.cardDataSO);
            levelManager.UpdateFillCount(1);
            levelManager.RemoveCardFromList(dragDropCard.cardDataSO);

            if(levelManager.currentStage == Stage.Stage4) Destroy(dragDropCard.gameObject);
            else
            {
                //Card ค่่อยๆจางลง
                CanvasGroup canvasGroup = dragDropCard.gameObject.GetComponent<CanvasGroup>(); // อ้างอิง CanvasGroup ของคุณ
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.DOFade(0f, 0.75f) // ค่อย ๆ หายใน 0.75 วินาที
                    .SetEase(Ease.InOutSine)  // ใส่ Ease เพื่อให้ Smooth
                    .OnComplete(() =>
                    {
                    });
                            //ถ้าเป็นใบสุดท้ายจะสุ่มขึ้นมาใหม่
                levelManager.currentCardCount--;
                if(!levelManager.CheckAllCardCount())
                {
                levelManager.CheckCardCount();
                }

            }
        }
       }
    }

       private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                collision.GetComponent<DropBox>().img.GetComponent<Image>().color = dragColor;
                dropBox = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                //collision.gameObject.GetComponent<Image>().color = UiController.Instance.SetColorWithHex("#8C5E44");
                collision.GetComponent<DropBox>().img.GetComponent<Image>().color = Color.white;
                dropBox = null;
            }
        }
    

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace PersonalValue
{
    public class DragPrefabUn : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector3 offset;
        public DropBoxPriority dropBox;
        public DropBoxPriority formDropBox;
        public DragDropCard dragDropCard;
        public GameObject cardGroup;
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

        if(dropBox == formDropBox) 
        {
             Hide();
        }
        else
        {
          
          //ถ้าย้ายไป dropBox อันว่าง
          if(dropBox.cardName_Stage4 == null)
          {
            //Dropbox อันใหม่
            dropBox.cardName_Stage4 = formDropBox.cardName_Stage4;
            dropBox.transform.GetChild(0).GetComponent<Image>().sprite = formDropBox.cardName_Stage4.picture;

            //Dropbox อันเดิม
            formDropBox.cardName_Stage4 = null;
            formDropBox.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.cardDatabaseSO.nullSprite;
            
            Hide();
            Debug.Log("ว่าง");
            return;
          }
          //ถ้าย้ายไป dropBox อันไม่ว่าง สลับกัน
          if(dropBox.cardName_Stage4 != null)
          {
            Debug.Log("สลับ");
            // 🔁 เก็บข้อมูลของ dropBox ใหม่ไว้ชั่วคราว
            CardDataSO tempCard = dropBox.cardName_Stage4;

            // ✅ สลับข้อมูล DropBox ใหม่ → ใส่ข้อมูลจากเดิม (formDropBox)
            dropBox.cardName_Stage4 = formDropBox.cardName_Stage4;
            dropBox.transform.GetChild(0).GetComponent<Image>().sprite = formDropBox.cardName_Stage4.picture;

            // ✅ ใส่ tempCard กลับไปที่กล่องเดิม
            formDropBox.cardName_Stage4 = tempCard;
            formDropBox.transform.GetChild(0).GetComponent<Image>().sprite = tempCard.picture;

            Hide();
          }
        }
       }
    }
    
    public void Hide()
    {
        dropBox = null;
        formDropBox = null;
        this.gameObject.SetActive(false);
    }
       private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                collision.GetComponent<DropBoxPriority>().img.GetComponent<Image>().color = dragColor;
                dropBox = collision.GetComponent<DropBoxPriority>();
            }

            if(collision.tag == "CardGroup")
            {
                collision.GetComponent<Image>().color = dragColor;
                cardGroup = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                collision.GetComponent<DropBoxPriority>().img.GetComponent<Image>().color = Color.white;
                dropBox = null;
            }

            if(collision.tag == "CardGroup")
            {
                collision.GetComponent<Image>().color = Color.white;
                cardGroup = null;
            }
        }
    }
}

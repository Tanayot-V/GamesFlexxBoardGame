using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace  PersonalValue
{
    public class DragPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector3 offset;
        public GameObject dropBox;
        public DragDropCard dragDropCard;

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
       if(dropBox != null)
       {
         dropBox.GetComponent<DropBox>().cardDataSOList.Add(dragDropCard.cardDataSO);
         GameManager.Instance.levelManager.UpdateFillCount(1);
         GameManager.Instance.levelManager.RemoveCardFromList(dragDropCard.cardDataSO);
         Destroy(dragDropCard.gameObject);
        //ถ้าเป็นใบสุดท้ายจะสุ่มขึ้นมาใหม่
         GameManager.Instance.levelManager.currentCardCount--;
        if(!GameManager.Instance.levelManager.CheckAllCardCount())
        {
            GameManager.Instance.levelManager.CheckCardCount();
        }
       }
    }

       private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                collision.gameObject.GetComponent<Image>().color = Color.red;
                dropBox = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Menu")
            {
                //collision.gameObject.GetComponent<Image>().color = UiController.Instance.SetColorWithHex("#8C5E44");
                collision.gameObject.GetComponent<Image>().color = Color.white;
                dropBox = null;
            }
        }
    

    }
}

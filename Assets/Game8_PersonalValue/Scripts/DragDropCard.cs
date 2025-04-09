using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace  PersonalValue
{
    
    public class DragDropCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image img;
        public CardDataSO cardDataSO;
        private RectTransform mockupRect;

        private void Awake()
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameObject mockupDragCard = GameManager.Instance.levelManager.mockUpDragCard;
            mockupDragCard.SetActive(true);
            mockupDragCard.GetComponent<DragPrefab>().dragDropCard = this;
            mockupDragCard.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;
            mockupDragCard.transform.localScale = new Vector3(1, 1, 1);

            Transform rectTransform = mockupDragCard.GetComponent<Transform>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rectTransform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

            mockupRect = mockupDragCard.GetComponent<RectTransform>();
            this.GetComponent<Image>().color = new Color(145, 145, 145, 0.5f);
        }

        public void OnDrag(PointerEventData eventData)
        {
           if (mockupRect != null)
            {
                Vector3 worldPos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    mockupRect, eventData.position, GameManager.Instance.levelManager.mainCamera, out worldPos))
                {
                    mockupRect.position = worldPos;
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            this.GetComponent<Image>().color = Color.white;
            if (mockupRect != null)
            {
                mockupRect.GetComponent<DragPrefab>().SetCardToBox();
                //mockupRect.gameObject.SetActive(false);

                mockupRect.transform.localScale = Vector3.one;
                mockupRect.transform.DOScale(Vector3.zero, 0.25f)
                .SetEase(Ease.Linear)
                .OnComplete(()=>{
                    mockupRect.gameObject.SetActive(false);
                });; // ค่อยๆ ขยายแบบ Pop-up
            }
        }
    }
}

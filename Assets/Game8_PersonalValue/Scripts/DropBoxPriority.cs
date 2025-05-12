using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PersonalValue;
using DG.Tweening;

public class DropBoxPriority : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image img;
    public TMPro.TextMeshProUGUI text;
    public string dropName;
    public List<CardDataSO> cardDataSOList = new List<CardDataSO>();
    public CardDataSO cardName_Stage4;
    private RectTransform mockupRect;

    void Start()
    {
    }

    void Update()
    {
        
    }
    
    public void UninstallCardStage4()
    {
        /*
        if (cardName_Stage4 != null)
        {
            GameManager.Instance.levelManager.UninstallCardStage4(this,cardName_Stage4);
            cardName_Stage4 = null;
        }
        else
        {
            Debug.Log("Card is null");
        }*/
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       GameObject mockupDragCard = GameManager.Instance.levelManager.mockUpDragCardUn;
            mockupDragCard.SetActive(true);
            //mockupDragCard.GetComponent<DragPrefab>().dragDropCard = this;
            mockupDragCard.GetComponent<Image>().sprite = this.transform.GetChild(0).GetComponent<Image>().sprite;
            mockupDragCard.transform.localScale = new Vector3(1, 1, 1);

            Transform rectTransform = mockupDragCard.GetComponent<Transform>();
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rectTransform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

            mockupRect = mockupDragCard.GetComponent<RectTransform>();
            img.color = new Color(145, 145, 145, 0.5f);

            mockupDragCard.GetComponent<DragPrefabUn>().formDropBox = this;
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
                if(mockupRect.GetComponent<DragPrefabUn>().dropBox != null)
                {
                    mockupRect.GetComponent<DragPrefabUn>().SetCardToBox();
                }
                //UninstallCard
                else if(mockupRect.GetComponent<DragPrefabUn>().cardGroup != null)
                {
                    Debug.Log("ถอดการติดตั้ง");
                    LevelManager levelManager = GameManager.Instance.levelManager;
                    GameObject newCard = levelManager.CreateCard(levelManager.cardPrefab,levelManager.priorityParent,mockupRect.GetComponent<DragPrefabUn>().formDropBox.GetComponent<DropBoxPriority>().cardName_Stage4);
                    GameManager.Instance.gridScrollController.InsertCardAtCurrentRow(newCard);

                    mockupRect.GetComponent<DragPrefabUn>().formDropBox.cardName_Stage4 = null;
                    mockupRect.GetComponent<DragPrefabUn>().formDropBox.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.cardDatabaseSO.nullSprite;
                    mockupRect.GetComponent<DragPrefabUn>().Hide();
                }
                else
                {
                    mockupRect.GetComponent<DragPrefabUn>().Hide();
                }
            }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
      
    }

}

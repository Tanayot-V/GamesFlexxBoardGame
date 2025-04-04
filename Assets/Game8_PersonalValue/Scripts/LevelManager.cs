using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace PersonalValue
{    
    public class LevelManager : MonoBehaviour
    {
        [Header("Stage1 Model")]
        public Camera mainCamera;
        public GameObject canvasGame;
        public GameObject boxPrefab;
        public GameObject boxParent;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public GameObject mockUpDragCard;
        private readonly int maxCardCount = 5;
        public int currentCardCount = 0;

        [Header("Stage1 State")]
        public List<GameObject> boxList = new List<GameObject>();
        public List<CardDataSO> cardDataList = new List<CardDataSO>();

        #region Stage1
        public void Start()
        {
            Stage1();
        }

        public void Stage1()
        {
            GameManager.Instance.countdownTimer.StartCountdown();
            PersonalValueDatabaseSO databaseSO = GameManager.Instance.cardDatabaseSO;
            //Create Cards
            GameManager.Instance.cardDatabaseSO.cardDataSO.ToList().ForEach(o =>
            {
                if (o != null)
                {
                    cardDataList.Add(o);
                }
            });

            //Create Box
            UiController.Instance.DestorySlot(boxParent);
            int index = 0;
            databaseSO.boxsNameList_1.ToList().ForEach(o =>
            {
                GameObject box = UiController.Instance.InstantiateUIView(boxPrefab ,boxParent);
                box.name = "boxStage1_" + index;
                box.GetComponent<DropBox>().dropName = o;
                index++;
                box.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = o;
                if (o != null)
                {
                    boxList.Add(box);
                }
            });
            RerollCard();
      }

        public void RerollCard()
        {
            GameManager.Instance.countdownTimer.StartCountdown();
            Shuffle(cardDataList);
            UiController.Instance.DestorySlot(cardParent);
            for(int i = 0; i < maxCardCount; i++)
            {
                GameObject card = UiController.Instance.InstantiateUIView(cardPrefab ,cardParent);
                card.name = cardDataList[i].name;
                card.GetComponent<Image>().sprite = cardDataList[i].picture;
                card.GetComponent<DragDropCard>().cardDataSO = cardDataList[i];
            }
            currentCardCount = maxCardCount;
        }

        public void Shuffle(List<CardDataSO> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rand = Random.Range(i, list.Count);
                CardDataSO temp = list[i];
                list[i] = list[rand];
                list[rand] = temp;
            }

            Debug.Log("✅ สุ่มการ์ดเสร็จแล้ว");
        }

        public void RemoveCardFromList(CardDataSO cardDataSO)
        {
            cardDataList.Remove(cardDataSO);
            Debug.Log("🟢 ลบการ์ด " + cardDataSO.name + " ออกจากรายการการ์ด");
        }

        public bool IsLastCard()
        {
            if (cardParent.transform.childCount <= 0)
            {
                Debug.Log("🟢 การ์ดหมดแล้ว");
                return true;
            }
            else
            {
                Debug.Log("🟢 ยังมีการ์ดเหลืออยู่");
                return false;
            }
        }

        public void CheckCardCount()
        {
            if (currentCardCount <= 0)
            {
                Debug.Log("🟢 การ์ดหมดแล้ว");
                RerollCard();
            }
        }
      #endregion
    }
}

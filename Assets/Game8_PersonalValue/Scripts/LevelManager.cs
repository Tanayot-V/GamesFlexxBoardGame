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
            PersonalValueDatabaseSO databaseSO = GameManager.Instance.cardDatabaseSO;
            //Create Box
            UiController.Instance.DestorySlot(boxParent);
            int index = 0 ;
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

            //Create Cards
            databaseSO.cardDataSO.ToList().ForEach(o =>
            {
                if (o != null)
                {
                    cardDataList.Add(o);
                }
            });

            UiController.Instance.DestorySlot(cardParent);
            for(int i = 0; i < maxCardCount; i++)
            {
                GameObject card = UiController.Instance.InstantiateUIView(cardPrefab ,cardParent);
                card.name = cardDataList[i].name;
                card.GetComponent<Image>().sprite = cardDataList[i].picture;
                card.GetComponent<DragDropCard>().cardDataSO = cardDataList[i];
            }
      }
      #endregion
    }
}

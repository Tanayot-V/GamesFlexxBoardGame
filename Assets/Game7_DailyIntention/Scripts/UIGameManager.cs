using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DailyIntention
{
    public class UIGameManager : MonoBehaviour
    {
       public GameObject lobbyPanel;
       public GameObject gameplayPanel;
       public GameObject backPanel;

       [Header("head Panel")]
       public GameObject headPanel;
       public Image bgIMG;
       public Image couldIMG;
       public Image textIMG;

        [Header("ans Panel")]
        
        public GameObject ansPanel;
        public Image textAnsIMG;


        void Start()
        {
            lobbyPanel.SetActive(true);
            gameplayPanel.SetActive(false);
            headPanel.SetActive(false);
            ansPanel.SetActive(false);
            backPanel.SetActive(false);
        }

        void Update()
        {
            
        }

        public void RandomClick()
        {
            GameManager.Instance.levelManager.RandomData();
            SettingUPGameplayPage();
            gameplayPanel.SetActive(true);
            headPanel.SetActive(true);

            void SettingUPGameplayPage()
            {
                LevelManager levelManager = GameManager.Instance.levelManager;
                bgIMG.sprite = levelManager.intentionDatabaseSO.GetBGData(levelManager.currentDataSO.bGType).sprite;
                couldIMG.sprite = levelManager.intentionDatabaseSO.GetCloudData(levelManager.currentDataSO.cloudType).sprite;
                textIMG.sprite = levelManager.currentDataSO.txHeadIMG;
                textAnsIMG.sprite = levelManager.currentDataSO.txAnswerIMG;
            }
        }

        public void HeadPageClick()
        {
            headPanel.SetActive(false);
            ansPanel.SetActive(true);
        }

        public void AnsClick()
        {
            headPanel.SetActive(true);
            ansPanel.SetActive(false);
        }

        public void LoadSecene()
        {
            DataCenterManager.Instance.LoadSceneByName("Game7_DailyIntention");
        }
    }
}

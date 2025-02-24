using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace DailyIntention
{
    public class UIGameManager : MonoBehaviour
    {
       public GameObject lobbyPanel;
       public GameObject gameplayPanel;
       public GameObject backPanel;

       public GameObject allCardsPanel;
       public GameObject uiEffectPanel;
       private bool isCanClick = true;
       public Animator animCard;

       [Header("head Panel")]
       public GameObject headPanel;
       public Image bgIMG;
       public Image couldIMG;
       public Image textIMG;
       public Image bgHeadIMG;

        [Header("ans Panel")]
        public GameObject ansPanel;
        public Image textAnsIMG;

        [Header("all crads Panel")]
        public GameObject allCardparent;
        public GameObject cardSlotPrefab;

        void Start()
        {
            HideAllPage();
            lobbyPanel.SetActive(true);
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
            ShowEffect();
            SkipToEndOfAnimation("OpenHead");
        }

        public void QClick()
        {
            GameManager.Instance.levelManager.NextData();
            SettingUPGameplayPage();
            gameplayPanel.SetActive(true);
            headPanel.SetActive(true);
        }

        public void IndexClick(IntentionDataSO _intentionDataSO)
        {
            GameManager.Instance.levelManager.currentDataSO = _intentionDataSO;
            SettingUPGameplayPage();
            HideAllPage();
            gameplayPanel.SetActive(true);
            headPanel.SetActive(true);
            ShowEffect();
            SkipToEndOfAnimation("OpenHead");
        }

        public void HeadPageClick()
        {
            if(!isCanClick) return;
            headPanel.SetActive(false);
            ansPanel.SetActive(true);
            animCard.SetBool("isClick",true);
        }

         void SettingUPGameplayPage()
        {
            LevelManager levelManager = GameManager.Instance.levelManager;
            bgIMG.sprite = levelManager.intentionDatabaseSO.GetBGData(levelManager.currentDataSO.bGType).sprite;
            couldIMG.sprite = levelManager.intentionDatabaseSO.GetCloudData(levelManager.currentDataSO.cloudType).sprite;
            textIMG.sprite = levelManager.currentDataSO.txHeadIMG;
            textAnsIMG.sprite = levelManager.currentDataSO.txAnswerIMG;
            bgHeadIMG.sprite = bgIMG.sprite;
        }

        public void AnsClick()
        {
            if(!isCanClick) return;
            headPanel.SetActive(true);
            ansPanel.SetActive(false);
            animCard.SetBool("isClick",false);
        }

        public void LoadSecene()
        {
            DataCenterManager.Instance.LoadSceneByName("Game7_DailyIntention");
        }

        public void BackButton()
        {
            HideAllPage();
            lobbyPanel.SetActive(true);
        }

        public void ShowAllPage()
        {
            HideAllPage();
            allCardsPanel.SetActive(true);
            
            UiController.Instance.DestorySlot(allCardparent);
            GameManager.Instance.levelManager.intentionDatabaseSO.pageDatas.ToList().ForEach(x => {
                GameObject slot = UiController.Instance.InstantiateUIView(cardSlotPrefab,allCardparent);
                slot.GetComponent<IntentionSlot>().currentDataSO = x;
            });
        }

        private void HideAllPage()
        {
            lobbyPanel.SetActive(false);
            gameplayPanel.SetActive(false);
            headPanel.SetActive(false);
            ansPanel.SetActive(false);
            backPanel.SetActive(false);
            allCardsPanel.SetActive(false);
        }

        private void ShowEffect()
        {
           isCanClick = false;
            uiEffectPanel.SetActive(true);
            uiEffectPanel.GetComponent<CanvasGroupTransition>().FadeOut(()=>{
                 uiEffectPanel.SetActive(false);
                 isCanClick = true;
            });
        }

        private void SkipToEndOfAnimation(string animationName)
        {
            animCard.Play(animationName, 0, 1f); // 1f คือเวลาสุดท้ายของคลิป
            animCard.Update(0); // บังคับให้ Animator อัพเดททันที
        }
    }
}

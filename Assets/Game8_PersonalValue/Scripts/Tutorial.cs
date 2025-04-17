using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

namespace  PersonalValue
{
    public class Tutorial : MonoBehaviour
    {
        public GameObject tutorialPageGroup;
        public Image imgBG_1;
        public Image imgBG_2;
        public Image imageTutorial_1;
        public Image imageTutorial_2;
        public Sprite[] sprites;       // รายการภาพ
        private int currentIndex = 0;

        #region  Tutorial 1
        public void InitTutorial()
        {
            tutorialPageGroup.SetActive(true);
            imgBG_1.gameObject.SetActive(true);
            imgBG_2.gameObject.SetActive(false);

            imageTutorial_1.GetComponent<CanvasGroup>().alpha = 0;
            imageTutorial_1.GetComponent<CanvasGroup>().DOFade(1,2f).OnComplete(()=>{ UpdateImage(); });
        }

        void UpdateImage()
        {
            if (sprites.Length > 0 && imageTutorial_1 != null)
            {
                imageTutorial_1.sprite = sprites[currentIndex];
            }
        }

        public void NextImage()
        {
            currentIndex++;
            if (currentIndex >= sprites.Length)
            {
                //currentIndex = 0; // วนกลับไปภาพแรก
                HideTutorialButton();
            }
            else  UpdateImage();
        }

        public void PreviousImage()
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = 0; // วนไปภาพสุดท้าย
            UpdateImage();
        }
        #endregion

        #region Tutorial 2
         public void InitTutorial2()
        {
            imgBG_2.sprite = null;
            tutorialPageGroup.SetActive(true);
            imgBG_1.gameObject.SetActive(false);
            imgBG_2.gameObject.SetActive(true);

            imageTutorial_2.GetComponent<CanvasGroup>().alpha = 0;
            imageTutorial_2.GetComponent<CanvasGroup>().DOFade(1,2f);
        }
        #endregion

        public void HideTutorialButton()
        {
            GameManager gameManager = GameManager.Instance;
            switch (gameManager.levelManager.currentStage)
            {
                case Stage.Stage1:
                    gameManager.tutorial.tutorialPageGroup.SetActive(false);
                    gameManager.levelManager.Stage1();
                    Debug.Log("Stage1");
                break;
            }
        }
    }
}

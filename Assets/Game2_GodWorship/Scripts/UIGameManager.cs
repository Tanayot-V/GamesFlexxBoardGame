using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace GodWarShip
{
    public class UIGameManager : MonoBehaviour
    {
        private string gameSceneName = "Game2_GodWorship";
        public GameObject gameplayPanel;
        public GameObject loadingPanel;
        public GameObject showGO;
        public GameObject backGO;
        public GameObject backPanel;
        public GameObject coverBigShowGO;
        public Sprite coverIMG;
        public GameObject imgChilds;
        public GameObject cardSlot;
        public List<Button> buttons = new List<Button>();
        private int currentIndex = 0; 
        
        public GameObject reloadGO;
        public GameObject reloadComfirmPanel;

        [Header("Lobby")]
        public GameObject lobbyPanel;
        public Mode mode;
        public GridLayoutGroup gridLayoutGroup;

        private void Start() {
          showGO.transform.GetChild(1).GetComponent<Image>().sprite = coverIMG;
          ShowLoading();
          reloadGO.SetActive(false);
          reloadComfirmPanel.SetActive(false);
          coverBigShowGO.SetActive(true);
          lobbyPanel.SetActive(true);
          gameplayPanel.SetActive(false); 
          backPanel.SetActive(false);
          mode = Mode.Normal;
          UiController.Instance.SetButtonGroup("G2SelectMode","1");
        }

        public void ShowLoading(System.Action _action = null)
        {
            loadingPanel.SetActive(true);
            StartCoroutine(UiController.Instance.WaitForSecond(1,()=>{
                loadingPanel.SetActive(false);
                if(_action != null) _action(); 
            }));
        }
        
        public void SelectMode(string _mode)
        {
            switch(_mode)
            {
                case "Easy":
                    mode = Mode.Easy;
                    break;
                case "Normal":
                    mode = Mode.Normal;
                    break;
                case "Hard":
                    mode = Mode.Hard;
                    break;
            }
        }

        public void StartButton()
        {
            lobbyPanel.SetActive(false);
            ShowLoading();
            gameplayPanel.SetActive(true);   
            ClearUI();
            switch(mode)
            {
                case Mode.Easy:
                    gridLayoutGroup.constraintCount = 3;
                    gridLayoutGroup.cellSize = new Vector2(300,400);
                    gridLayoutGroup.spacing = new Vector2(10,10);
                    gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;

                    GameManager.Instance.levelDataManager.InitGameEasyMode();
                    break;
                case Mode.Normal:
                    gridLayoutGroup.constraintCount = 4;
                    gridLayoutGroup.cellSize = new Vector2(230,313);
                    gridLayoutGroup.spacing = new Vector2(10,10);
                    gridLayoutGroup.childAlignment = TextAnchor.UpperCenter;

                    GameManager.Instance.levelDataManager.InitGameNormalMode();
                    break;
                case Mode.Hard:
                    gridLayoutGroup.constraintCount = 5;
                    gridLayoutGroup.cellSize = new Vector2(178,240);
                    gridLayoutGroup.spacing = new Vector2(10,10);
                    gridLayoutGroup.childAlignment = TextAnchor.MiddleCenter;

                    GameManager.Instance.levelDataManager.InitGameHardMode();
                    break;
            }

            void ClearUI()
            {
                UiController.Instance.DestorySlot(imgChilds);
                currentIndex = 0;
                reloadGO.SetActive(false);
                reloadComfirmPanel.SetActive(false);
                backPanel.SetActive(false);
            }
        }

        public void SetAllIMG(List<CardSO> _cardSOs)
        {
            UiController.Instance.DestorySlot(imgChilds);
            int index = 0;
            buttons.Clear();
          _cardSOs.ForEach(o => {
            CardWorshipSlot cardWorshipSlot = UiController.Instance.InstantiateUIView(cardSlot,imgChilds).GetComponent<CardWorshipSlot>();
            cardWorshipSlot.pictureIMG.sprite = _cardSOs[index].picture;
            cardWorshipSlot.cardSO = o;
            cardWorshipSlot.gameObject.name = index.ToString();
            cardWorshipSlot.InitSlot(index);
            buttons.Add(cardWorshipSlot.GetComponent<Button>());
            index++;
          });
          showGO.transform.GetChild(0).GetComponent<Image>().sprite = _cardSOs[0].picture;
          SetButtonInteractivity();
          
          Button btnChar = ButtonGroupManager.Instance.GetButton("SelectedAura", "0");
          if (btnChar != null) ButtonGroupManager.Instance.Select(btnChar.GetComponent<ButtonGroup>());
        }

         public void OnButtonPressed(int buttonIndex)
        {
          // ตรวจสอบว่าปุ่มที่กดตรงกับลำดับหรือไม่
          if (buttonIndex == currentIndex)
          {
              Debug.Log($"Correct button {buttonIndex} pressed!");
              currentIndex++;

              // ตรวจสอบว่ากดครบทุกลำดับหรือยัง
              if (currentIndex >= buttons.Count)
              {
                  Debug.Log("All buttons pressed in order! Sequence complete.");
                  return;
              }

              // ตั้งค่าปุ่มให้สามารถกดได้เฉพาะลำดับถัดไป
              SetButtonInteractivity();
          }
          else
          {
              Debug.LogWarning($"Wrong button {buttonIndex} pressed! Please press button {currentIndex}.");
          }
        }

      private void SetButtonInteractivity()
      {
          for (int i = 0; i < buttons.Count; i++)
          {
              // เปิดการกดได้เฉพาะปุ่มที่อยู่ในลำดับปัจจุบัน
              if(!buttons[i].GetComponent<CardWorshipSlot>().isOpen) buttons[i].interactable = (i == currentIndex);
          }
      }

      public void FaedShowCover()
      {
        showGO.transform.GetChild(1).GetComponent<CanvasGroupTransition>().FadeOut();
      }

      public void SetShowIMG(Sprite _sprite)
      {
        showGO.transform.GetChild(0).GetComponent<Image>().sprite = _sprite;
      }

      public void LoadSeceneGame()
      {
        DataCenterManager.Instance.LoadSceneByName(gameSceneName);
      }

      public void LoadSecene(string _name)
      {
        DataCenterManager.Instance.LoadSceneByName(_name);
      }

      public void ShowReloadComfirmPanelButton()
      {
        reloadComfirmPanel.SetActive(true);
        reloadComfirmPanel.GetComponent<CanvasGroupTransition>().FadeIn();
      }

      public void ShowBackComfirmPanelButton()
      {
        backPanel.SetActive(true);
        backPanel.GetComponent<CanvasGroupTransition>().FadeIn();
      }
    }
}

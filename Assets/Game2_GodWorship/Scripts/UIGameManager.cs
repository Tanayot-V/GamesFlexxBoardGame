using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using SpotTheMissing;

namespace GodWarShip
{
    public class UIGameManager : MonoBehaviour
    {
        public GameObject loadingPanel;
        public GameObject showGO;
        public Sprite coverIMG;
        public GameObject imgChilds;
        public GameObject cardSlot;
        public List<Button> buttons = new List<Button>();
        private int currentIndex = 0; 
        
        public GameObject reloadGO;
        public GameObject reloadComfirmPanel;

        private void Start() {
          showGO.transform.GetChild(1).GetComponent<Image>().sprite = coverIMG;
          ShowLoading();
          reloadGO.SetActive(false);
          reloadComfirmPanel.SetActive(false);
        }

         public void ShowLoading(System.Action _action = null)
        {
            loadingPanel.SetActive(true);
            StartCoroutine(UiController.Instance.WaitForSecond(1,()=>{
                loadingPanel.SetActive(false);
                if(_action != null) _action(); 
            }));
        }
        
        public void SetAllIMG(List<CardSO> _cardSOs)
        {
            UiController.Instance.DestorySlot(imgChilds);
            int index = 0;
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

      public void LoadSecene(string _name)
      {
        DataCenterManager.Instance.LoadSceneByName(_name);
      }

      public void ShowReloadComfirmPanelButton()
      {
        reloadComfirmPanel.SetActive(true);
        reloadComfirmPanel.GetComponent<CanvasGroupTransition>().FadeIn();
      }
    }
}

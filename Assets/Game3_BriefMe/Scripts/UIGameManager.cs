using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BriefMe
{
    public class UIGameManager : MonoBehaviour
    {
       [SerializeField] GameObject lobbyPanelGO;
       [SerializeField] GameObject gamePlayPanelGO;
       [SerializeField] GameObject loadingPanelGO;
       [SerializeField] Image showIMG;

       public void InitStartGameUI()
       {
            lobbyPanelGO.SetActive(true);
            gamePlayPanelGO.SetActive(false);
            loadingPanelGO.SetActive(false);
       }

        public void LoadSecene(string _name)
        {
            DataCenterManager.Instance.LoadSceneByName(_name);
        }

        public void ShowLoading(System.Action _action = null)
        {
            loadingPanelGO.SetActive(true);
            StartCoroutine(UiController.Instance.WaitForSecond(1,()=>{
                loadingPanelGO.SetActive(false);
                if(_action != null) _action(); 
            }));
        }

        public void PictureTypeButton()
        {
            lobbyPanelGO.SetActive(false);
            gamePlayPanelGO.SetActive(true);
            GameManager.Instance.levelManager.PictureShow(showIMG);
            showIMG.GetComponent<CanvasGroupTransition>().FadeIn();
        }

        public void LineTypeButton()
        {
            lobbyPanelGO.SetActive(false);
            gamePlayPanelGO.SetActive(true);
            GameManager.Instance.levelManager.LineShow(showIMG);
            showIMG.GetComponent<CanvasGroupTransition>().FadeIn();
        }

        public void BackButton()
        {
            lobbyPanelGO.SetActive(true);
            gamePlayPanelGO.SetActive(false);
        }
    }
}

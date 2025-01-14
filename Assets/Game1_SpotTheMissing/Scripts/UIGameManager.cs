using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpotTheMissing
{
    public class UIGameManager : MonoBehaviour
    {
        [Header("Lobby")]
        public GameObject selectStagePanel;
        public GameObject selectPlayerPanel;

        public GameObject[] selecetButtons;

        [Header("Game World")]
        public GameObject loadingObj;
        public GameObject gamePanel;
        public GameObject gameworldParent;

        [Header("UI Player A")]
        public GameObject playerAPanel;
        public Button okButton;
        public TMPro.TextMeshProUGUI okTX;
        
        [Header("UI Player B")]
        public GameObject playerBPanel;

        [Header("Summary")]
        public GameObject summaryPanel;
        public Sprite[] ifCorrects;
        public DisplaySummary[] displaySummaries;
        

        public void LoadSceneByName(string _name)
        {
            DataCenterManager.Instance.LoadSceneByName(_name);
        }

        public void SelectStageButton(StageModelSO _stageModelSO)
        {
            GameManager.Instance.SetStageModelSO(_stageModelSO);
        }

    #region  Player A
        public void ApcectButton()
        {
            if(GameManager.Instance.levelManager.IsLastStage())
            {
                ShowSummary();
                GameManager.Instance.SetCurrentStageSummary();
                return;
            }
            if(!GameManager.Instance.levelManager.currentStagePrefabA.IsCanNextLevel()) 
            {
                return;
            }
            else
            {
                GameManager.Instance.SetCurrentStageSummary();
                GameManager.Instance.NextLevel();
            }
        }

        public void OKButtonGray()
        {
            okButton.GetComponent<Image>().color = Color.gray;
        }
        public void OKButtonGreen()
        {
            okButton.GetComponent<Image>().color = Color.green;
        }
    #endregion

    #region Player B
    public void NextBButton()
    {
        GameManager.Instance.NextLevel();
        if(GameManager.Instance.IsLastStage()) DataCenterManager.Instance.LoadSceneByName("Game1_MainGame");
    }
    public void BackBButton()
    {
        GameManager.Instance.BackLevel();
        if(GameManager.Instance.levelManager.roundIndex <= 0) DataCenterManager.Instance.LoadSceneByName("Game1_MainGame");
    }
    #endregion

    #region  Lobby
        public void SelectPlayerButton(string _id)
        {
            switch(_id)
            {
                case "A":
                    GameManager.Instance.SetPlayer(Player.A);
                break;
                case "B":
                    GameManager.Instance.SetPlayer(Player.B);
                break;
            }
        }

        public void OpenSelectStagePanel()
        {
            gamePanel.SetActive(false);
            selectPlayerPanel.SetActive(false);
            selectStagePanel.SetActive(true);
            summaryPanel.SetActive(false);
        }

        public void OpenSelectPlayerPanel()
        {    
            gamePanel.SetActive(false);
            selectStagePanel.SetActive(false);
            selectPlayerPanel.SetActive(true);
        }

        public void CloseSelectPlayerPanel()
        {    
            selectStagePanel.SetActive(false);
            selectPlayerPanel.SetActive(false);
            GameManager.Instance.ShowLoading();
            GameManager.Instance.InitGameWorld();
            GameManager.Instance.SetupStageSummary();
        }
       
        public void LoadSecene(string _name)
        {
            DataCenterManager.Instance.LoadSceneByName(_name);
        }
    #endregion
    
    #region  Summary
    public void ShowSummary()
    {
        summaryPanel.SetActive(true);
        int index = 0;

        foreach (var stageSummary in GameManager.Instance.levelManager.stageSummaries)
        {
            var displaySummary = displaySummaries[index];
            displaySummary.selectedIMG.sprite = stageSummary.selectSP;
            displaySummary.correctedIMG.sprite = stageSummary.correctSP;

            // เช็คว่าถูกต้องหรือไม่และตั้งค่าภาพ ifCorrectIMG
            displaySummary.ifCorrectIMG.sprite = stageSummary.isCorrect ? ifCorrects[0] : ifCorrects[1];
            
            index++;
        }
    }
    #endregion
    }

}

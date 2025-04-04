using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SpotTheMissing
{
    public class UIGameManager : MonoBehaviour
    {
        [Header("Lobby")]
        public GameObject selectPlayerPanel;
        public GameObject selectStagePanel;
        public GameObject aPanel;
        public GameObject bPanel;

        public GameObject[] selecetStageButtons;
        public GameObject[] selecetPlayerButtons;

        [Header("GamePlay")]
        public GameObject loadingObj;
        public GameObject gamePanel;
        public GameObject gameworldParent;
        public TMPro.TextMeshProUGUI headlineGPTX;
        public Material materialGray;

        [Header("UI Player A")]
        public GameObject playerAPanel;
        public Button okButton;
        public TMPro.TextMeshProUGUI okTX;
        public GameObject scoreSummaryButton;
        public GameObject comfirmScorePanel;
        
        [Header("UI Player B")]
        public GameObject playerBPanel;

        [Header("Summary")]
        public GameObject summaryPanel;
        public TMPro.TextMeshProUGUI headlineSUMTX;

        public Sprite[] ifCorrects;
        public DisplaySummary[] displaySummaries;
        public GameObject[] animationQueue;
        public Sprite nullSP;
        

        public void LoadSceneByName(string _name)
        {
            DataCenterManager.Instance.LoadSceneByName(_name);
        }

    #region  Player A
        public void ApcectButton()
        {
            if(!GameManager.Instance.levelManager.currentStagePrefabA.IsCanNextLevel()) 
            {
                return;
            }
            else
            {
                if(GameManager.Instance.levelManager.IsLastStage())
                {
                    OpenSummaryPanel();
                    return;
                }
                else
                {
                    GameManager.Instance.SetCurrentStageSummary();
                    GameManager.Instance.NextLevel();
                }
            }
        }

        public void OKButtonGray()
        {
            okButton.targetGraphic.material = materialGray;
        }
        public void OKButtonGreen()
        {
            okButton.targetGraphic.material = null;
        }
        public void OpenComfirmScorePanel()
        {
            comfirmScorePanel.SetActive(true);
            comfirmScorePanel.GetComponent<CanvasGroupTransition>().FadeIn();
        }

        public void OpenSummaryPanel()
        {
            GameManager.Instance.SetCurrentStageSummary();
            GameManager.Instance.ShowLoading(()=>{
                ShowSummary();
            });
        }

    #endregion

    #region Player B
    public void NextBButton()
    {
        if(GameManager.Instance.IsLastStage()) 
        {
            DataCenterManager.Instance.LoadSceneByName(GameManager.Instance.gameSeneceName);
        }
        else GameManager.Instance.NextLevel();
    }
    public void BackBButton()
    {
        if(GameManager.Instance.levelManager.roundIndex <= 0) 
        {
            DataCenterManager.Instance.LoadSceneByName(GameManager.Instance.gameSeneceName);
        }
        else GameManager.Instance.BackLevel();
    }
    #endregion

    #region  Lobby
        public void SelectStageButton(StageModelSO _stageModelSO)
        {
            GameManager.Instance.SetStageModelSO(_stageModelSO);
        }

        public void SelectPlayerButton(string _id)
        {
            switch(_id)
            {
                case "A":
                    GameManager.Instance.SetPlayer(Player.A);
                    aPanel.SetActive(true);
                    bPanel.SetActive(false);
                break;
                case "B":
                    GameManager.Instance.SetPlayer(Player.B);
                    aPanel.SetActive(false);
                    bPanel.SetActive(true);
                break;
            }
        }

        public void OpenSelectStagePanel()
        {
            gamePanel.SetActive(false);
            selectPlayerPanel.SetActive(false);
            selectStagePanel.SetActive(true);
            summaryPanel.SetActive(false);

            selecetStageButtons.ToList().ForEach(o => { o.GetComponent<ButtonClickSlot>().HideAuraStage();});
            selecetStageButtons[0].GetComponent<Button>().onClick.Invoke();
        }

        public void OpenSelectPlayerPanel()
        {    
            gamePanel.SetActive(false);
            selectStagePanel.SetActive(false);
            selectPlayerPanel.SetActive(true);
            summaryPanel.SetActive(false);

            selecetPlayerButtons.ToList().ForEach(o => {o.GetComponent<ButtonClickSlot>().HideAuraStage();});
            selecetPlayerButtons[0].GetComponent<Button>().onClick.Invoke();
        }
       
        public void CloseSelectStagePanel()
        {
            selectStagePanel.SetActive(false);
            selectPlayerPanel.SetActive(false);
            GameManager.Instance.SetupStageSummary();
            GameManager.Instance.InitGameWorld();
            GameManager.Instance.ShowLoading();
        }

        public void LoadSecene(string _name)
        {
            DataCenterManager.Instance.LoadSceneByName(_name);
        }
    #endregion
    
    #region  Summary
    public void ShowSummary()
    {
        headlineSUMTX.text = GameManager.Instance.levelManager.currentStage.summaryDisplay;
        summaryPanel.SetActive(true);
        int index = 0;

        foreach (var stageSummary in GameManager.Instance.levelManager.stageSummaries)
        {
            var displaySummary = displaySummaries[index];
            if(stageSummary.selectSP == null) 
            {
                displaySummary.selectedIMG.sprite = nullSP;
                displaySummary.correctedIMG.sprite = nullSP;
                displaySummary.ifCorrectIMG.sprite = nullSP;
            }
            else
            {
                displaySummary.selectedIMG.sprite = stageSummary.selectSP;
                displaySummary.correctedIMG.sprite = stageSummary.correctSP;
                Debug.Log("displaySummary.selectedIMG.: " +index+ stageSummary.selectSP.name);
                Debug.Log("index: " + index);

                if(stageSummary.roundID == "RoundModel_5") 
                {
                    displaySummary.selectedIMG.sprite = stageSummary.roundModelSO.GetItemSprite(stageSummary.selectID);
                    displaySummary.correctedIMG.sprite = stageSummary.roundModelSO.GetItemSprite(stageSummary.correctID);
                }
                
                // เช็คว่าถูกต้องหรือไม่และตั้งค่าภาพ ifCorrectIMG
                displaySummary.ifCorrectIMG.sprite = stageSummary.isCorrect ? ifCorrects[0] : ifCorrects[1];
            }
            index++;
        }
        //Animation
        StartCoroutine(PlayAnimation());

        IEnumerator PlayAnimation()
        {
            animationQueue.ToList().ForEach(o => {
                o.GetComponent<RectTransform>().localScale = Vector3.zero;
            });

            foreach (var o in animationQueue.ToList())
            {
                UITransition.Instance.ScaleOneSet(o, Vector3.zero, Vector3.one);
                yield return new WaitForSeconds(0.125f); // หน่วงเวลา 0.5 วินาที (ปรับได้ตามต้องการ)
            }
        }
    }
    #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheMissing
{
    public class GameManager : Singletons<GameManager>
    {
        public string gameSeneceName = "Game1_MainGame";
        public LevelManager levelManager;
        public UIGameManager uiGameManager;

        private void Start() {
            ShowLoading();
            ResetLobbySecene();
            UiController.Instance.DestorySlot(uiGameManager.gameworldParent);
        }

        public void InitGameWorld()
        {
            levelManager.currentRound = levelManager.stageSummaries[0].roundModelSO;
            uiGameManager.gamePanel.SetActive(true);
            uiGameManager.playerAPanel.SetActive(false);
            uiGameManager.playerBPanel.SetActive(false);
            uiGameManager.summaryPanel.SetActive(false);
            uiGameManager.comfirmScorePanel.SetActive(false);
            CreateStage();
        }

        private void CreateStage()
        {
            uiGameManager.headlineGPTX.text = "ด่านที่ "+ (levelManager.roundIndex + 1);
            GameObject gameOBJ;
            UiController.Instance.DestorySlot(uiGameManager.gameworldParent);
            if(levelManager.player == Player.A)
            {
                uiGameManager.playerAPanel.SetActive(true);
                gameOBJ = Instantiate(levelManager.currentRound.prefabA);
                levelManager.currentStagePrefabA = gameOBJ.GetComponent<StagePrefabA>();
                levelManager.currentStagePrefabA.ResetSelectedAll();
                uiGameManager.OKButtonGray();
                uiGameManager.okTX.text = "ยืนยัน";
                if(IsLastStage()) uiGameManager.scoreSummaryButton.SetActive(false);
                else uiGameManager.scoreSummaryButton.SetActive(true);
            }
            else
            {
                uiGameManager.playerBPanel.SetActive(true);
                gameOBJ = Instantiate(levelManager.currentRound.prefabB);
                levelManager.currentStagePrefabB = gameOBJ.GetComponent<StagePrefabB>();
                gameOBJ.GetComponent<StagePrefabB>().HideItemIsCorrect();
            }
            gameOBJ.transform.SetParent(uiGameManager.gameworldParent.transform);
            gameOBJ.transform.position = Vector3.zero;
            gameOBJ.transform.localScale = Vector3.one;
            gameOBJ.SetActive(true);
        }

        public void ShowLoading(System.Action _action = null)
        {
            uiGameManager.loadingObj.SetActive(true);
            StartCoroutine(UiController.Instance.WaitForSecond(1,()=>{
                uiGameManager.loadingObj.SetActive(false);
                if(_action != null) _action(); 
            }));
        }
        
        #region  LevelManager
        public void SetStageModelSO(StageModelSO _stageModelSO)
        {
            levelManager.SetStageModelSO(_stageModelSO);
        }
        public void SetRoundModelSO(RoundModelSO _roundModelSO)
        {
            levelManager.SetRoundModelSO(_roundModelSO);
        }
        public void SetPlayer(Player _player)
        {
            levelManager.SetPlayer(_player);
        }
        public void ResetLobbySecene()
        {
            levelManager.ResetLobbySecene();
        }
        public void SetupStageSummary()
        {
            levelManager.SetupStageSummary();
        }
        public void SetStageSummaryTOPrefab(ClickSlot _clickSlot)
        {
            levelManager.SetStageSummaryTOPrefab(_clickSlot);
            uiGameManager.OKButtonGreen();
        }

        public void SetCurrentStageSummary()
        {
            levelManager.SetCurrentStageSummary();
        }

        public void NextLevel()
        {
            levelManager.SetNextRound();
            CreateStage();
        }

        public void BackLevel()
        {
            levelManager.SetBackRound();
            CreateStage();
        }

        public bool IsLastStage()
        {
            return levelManager.IsLastStage();
        }
        #endregion

        #region  UIGameManager

        #endregion
    }
}

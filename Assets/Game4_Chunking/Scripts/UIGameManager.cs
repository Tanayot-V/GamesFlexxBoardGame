using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BriefMe;
using UnityEngine;
using UnityEngine.UI;

namespace Chunking
{
    public class UIGameManager : MonoBehaviour
    {
        [SerializeField] GameObject lobbyPanelGO;
        [SerializeField] GameObject gamePlayGO;
        [SerializeField] GameObject comfirmPanelGO;
        [SerializeField] GameObject loadingPanelGO;

        [Header("GamePlay")]
        [SerializeField] Image showBigIMG;
        [SerializeField] Image chunkIMG;
        [SerializeField] TMPro.TextMeshProUGUI[] chunksTX;
        public GameObject nextGO;
        public GameObject backGO;

        public void InitStartGame()
        {
            lobbyPanelGO.SetActive(true);
            gamePlayGO.SetActive(false);
            ShowLoading();
        }

        public void SetWayType(int _way)
        {
            GameManager.Instance.levelManager.SetWayType((WayType)_way);
        }

        public void StartGameClick()
        {
            GameManager.Instance.levelManager.StartGameClick();
            lobbyPanelGO.SetActive(false);
            gamePlayGO.SetActive(true);
        }

        public void BackComfirmClick()
        {
            gamePlayGO.SetActive(false);
            comfirmPanelGO.SetActive(false);
            lobbyPanelGO.SetActive(true);
            GameManager.Instance.levelManager.SetFristLobby();
            ShowLoading();
        }


        public void ComfirmPanelClick()
        {
            comfirmPanelGO.SetActive(true);
            comfirmPanelGO.GetComponent<CanvasGroupTransition>().FadeIn();
        }

        public void ShowGamePlay(ChunkingShowData _showData)
        {
            ChunkingData chunkingData = GameManager.Instance.GetChunkingData(_showData.type);
            showBigIMG.sprite = _showData.chunkingSO.picture;
            chunkIMG.sprite = chunkingData.showGameplay_SP;
            int index = 0;
            chunksTX.ToList().ForEach(o => {
                o.text = GameManager.Instance.GetChunkingData(_showData.type).descriptions[index];
                index++;
            });
        }

        public void NextClick()
        {
            GameManager.Instance.levelManager.NextClick();
        }

        public void BackClick()
        {
            GameManager.Instance.levelManager.BackClick();
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
    }
}

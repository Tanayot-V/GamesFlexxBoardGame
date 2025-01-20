using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chunking
{
    public class UIGameManager : MonoBehaviour
    {
        [SerializeField] GameObject lobbyPanelGO;
        [SerializeField] GameObject gamePlayGO;
            
        public void InitStartGame()
        {
            lobbyPanelGO.SetActive(true);
            gamePlayGO.SetActive(false);
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
    }
}

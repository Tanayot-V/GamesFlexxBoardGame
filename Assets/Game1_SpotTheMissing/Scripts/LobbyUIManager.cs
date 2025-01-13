using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpotTheMissing
{
public class LobbyUIManager : Singletons<LobbyUIManager>
{
    public GameObject selectStagePanel;
    public GameObject selectPlayerPanel;

    public GameObject[] selecetButtons;
    public void Start() {
        LevelManager.Instance.ResetLobbySecene();
    }

    public void SelectStageButton(StageModelSO _stageModelSO)
    {
            LevelManager.Instance.SetStageModelSO(_stageModelSO);
    }

   public void SelectPlayerButton(string _id)
   {
        switch(_id)
        {
            case "A":
                LevelManager.Instance.SetPlayer(Player.A);
            break;
            case "B":
                LevelManager.Instance.SetPlayer(Player.B);
            break;
        }
   }

    public void OpenSelectStagePanel()
    {
            selectStagePanel.SetActive(false);
            selectStagePanel.SetActive(true);
    }

    public void OpenSelectPlayerPanel()
    {    
            selectStagePanel.SetActive(false);
            selectPlayerPanel.SetActive(true);
    }

    public void LoadSecene(string _name)
    {
        DataCenterManager.Instance.LoadSceneByName(_name);
        LevelManager.Instance.SetupStageSummary();
    }
}
}

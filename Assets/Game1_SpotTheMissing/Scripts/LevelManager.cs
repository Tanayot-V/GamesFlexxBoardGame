using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SpotTheMissing
{
public enum Player
{
    A,
    B
}

[System.Serializable]
public class StageSummary
{
    public string roundID;
    public RoundModelSO roundModelSO;
    [Header("Selected")]
    public string selectID;
    public Sprite selectSP;

    [Header("Correct")]
    public string correctID;
    public Sprite correctSP;
    public bool isCorrect;

    public StageSummary(string _roundID,string _selectID,Sprite _selectSP,RoundModelSO _roundModelSO,bool _isCorrect)
    {
        roundID = _roundID;
        selectID = _selectID;
        selectSP = _selectSP;
        roundModelSO = _roundModelSO;
        isCorrect = _isCorrect;
    }

    public StageSummary(string _selectID,Sprite _selectSP,bool _isCorrect)
    {
        selectID = _selectID;
        selectSP = _selectSP;
        isCorrect = _isCorrect;
    }

    public void SetCorrectData(string _StageID)
    {
        correctID = roundModelSO.GetCorrectID(_StageID);
        correctSP = roundModelSO.GetCorrectSP(_StageID); 
    }
}

[System.Serializable]
public class PictureItems
{
    public string id;
    public Sprite sprite;
}

[System.Serializable]
public class DisplaySummary
{
    public string id;
    public Image selectedIMG;
    public Image correctedIMG;
    public Image ifCorrectIMG;
}

[System.Serializable]
public class CorrectData
{
    public string id;
    public string correctID;
    public Sprite correctIMG;
}
public class LevelManager : MonoBehaviour
{
   public Player player;
   public StageModelSO currentStage;
   public RoundModelSO currentRound;
   public StagePrefabA currentStagePrefabA;
   public StagePrefabB currentStagePrefabB;
   public int roundIndex;
    public List<StageSummary> stageSummaries = new List<StageSummary>();

    public void SetStageModelSO(StageModelSO _stageModelSO)
    {
        currentStage = _stageModelSO;
        //SetupStageSummary();
    }
    public void SetRoundModelSO(RoundModelSO _roundModelSO)
    {
        currentRound = _roundModelSO;
    }

    public void SetPlayer(Player _player)
    {
        player = _player;
    }

    public void ResetLobbySecene()
    {
        player = Player.A;
        GameManager.Instance.uiGameManager.OpenSelectPlayerPanel();
        stageSummaries.Clear();
        roundIndex = 0;
    }

    //เซตอัพตอนเริ่มต้นเกม
    public void SetupStageSummary()
    {
        stageSummaries.Clear();
        if(currentStage != null)
        {
            currentStage.roundModelSO.ToList().ForEach(o => {
                StageSummary _stageSummary = new StageSummary(o.id,string.Empty,null,o,false);
                _stageSummary.SetCorrectData(currentStage.id);
                stageSummaries.Add(_stageSummary);
            });
        }
        SetRoundModelSO(currentStage.roundModelSO[0]);
    }

    //กดตกลงแล้วบันทึกเข้าสู่ List
    public void SetCurrentStageSummary()
    {
       for (int i = 0; i < stageSummaries.Count; i++)
        {
            if (stageSummaries[i].roundID == currentStagePrefabA.stageSummary.roundID)
            {
                stageSummaries[i].selectID = currentStagePrefabA.stageSummary.selectID;
                stageSummaries[i].selectSP = currentStagePrefabA.stageSummary.selectSP;
                stageSummaries[i].isCorrect = currentStagePrefabA.stageSummary.isCorrect;
            }
        }
    }

    public void SetNextRound()
    {
        roundIndex += 1;
        if(roundIndex >= 5) roundIndex = 4;
        if(roundIndex < 0) roundIndex = 0;
        SetRoundModelSO(currentStage.roundModelSO[roundIndex]);
    }

    public void SetBackRound()
    {
        roundIndex -= 1;
        if(roundIndex >= 5) roundIndex = 4;
        if(roundIndex < 0) roundIndex = 0;
        SetRoundModelSO(currentStage.roundModelSO[roundIndex]);
    }
    public void SetStageSummaryTOPrefab(ClickSlot _clickSlot)
    {
        currentStagePrefabA.SetStageSummary(_clickSlot);
    }

    public bool IsLastStage()
    {
        bool isLastStge = false;
        if(roundIndex >= 4) isLastStge = true;
        return isLastStge;
    }
}
}

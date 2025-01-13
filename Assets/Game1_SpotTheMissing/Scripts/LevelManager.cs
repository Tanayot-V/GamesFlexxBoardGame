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
    public string selectID;
    public Sprite selectSP;
    public string correctID;
    public Sprite correctSP;
    public bool isCorrect;

    public StageSummary(string _roundID,string _selectID,Sprite _selectSP,string _correctID,Sprite _correctSP,bool _isCorrect)
    {
        roundID = _roundID;
        selectID = _selectID;
        selectSP = _selectSP;
        correctID = _correctID;
        correctSP = _correctSP;
        isCorrect = _isCorrect;
    }
}

public class LevelManager : Singletons<LevelManager>
{
   public Player player;
   public StageModelSO currentStage;
   public RoundModelSO currentRound;

    public List<StageSummary> stageSummaries = new List<StageSummary>();

     public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
             DataCenterManager.Instance.LoadSceneByName("Lobby");
        }
    }
    public void SetStageModelSO(StageModelSO _stageModelSO)
    {
        currentStage = _stageModelSO;
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
        LobbyUIManager.Instance. selecetButtons[0].GetComponent<Button>().onClick.Invoke();
        stageSummaries.Clear();
    }

    public void SetupStageSummary()
    {
        stageSummaries.Clear();
        if(currentStage != null)
        {
            currentStage.roundModelSO.ToList().ForEach(o => {
                stageSummaries.Add(new StageSummary(o.id,string.Empty,null,o.correctID,o.correctSprite,false));
            });
        }
    }

    public void SetStageSummary(string _id)
    {

    }
}
}

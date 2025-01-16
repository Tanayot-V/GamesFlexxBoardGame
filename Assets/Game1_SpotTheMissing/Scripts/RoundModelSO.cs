using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SpotTheMissing
{
  [CreateAssetMenu(fileName = "RoundModelSO", menuName = "SpotTheMissing/RoundModelSO", order = 1)]
  public class RoundModelSO : ScriptableObject
  {
    public string id;
    public GameObject prefabA;
    public GameObject prefabB;
    public CorrectData[] correctDatas;

    public bool IsCorrect(string _StageID,string _ITMid)
    {
      return correctDatas.Any(data => data.id == _StageID && data.correctID == _ITMid);
    }

   public string GetCorrectID(string _StageID)
    {
      switch(_StageID)
      {
        case "StageModel_1":
              return correctDatas[0].correctID;
        case "StageModel_2":
              return correctDatas[1].correctID;
        case "StageModel_3":
              return correctDatas[2].correctID;
      }
       return correctDatas[0].correctID;;
    }

   public Sprite GetCorrectSP(string _StageID)
    {
      switch(_StageID)
      {
        case "StageModel_1":
              return correctDatas[0].correctIMG;
        case "StageModel_2":
              return correctDatas[1].correctIMG;
        case "StageModel_3":
              return correctDatas[2].correctIMG;
      }
       return correctDatas[0].correctIMG;
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace  SpotTheMissing
{
    public class StagePrefabA : MonoBehaviour
    {
        public StageSummary stageSummary;
        public ClickSlot[] clickSlots;

        public void ResetSelectedAll()
        {
            clickSlots.ToList().ForEach(o =>{
                o.SelectedObjHide();
            });
        } 

        public void SetStageSummary(ClickSlot _clickSlot)
        {
            stageSummary.selectID = _clickSlot.name;
            stageSummary.selectSP = _clickSlot.GetComponent<SpriteRenderer>().sprite;
            stageSummary.isCorrect = stageSummary.roundModelSO.IsCorrect(GameManager.Instance.levelManager.currentStage.id,stageSummary.selectID);
        }

        public bool IsCanNextLevel()
        {
            bool isCanNextLevel = false;
            if(stageSummary.selectID.Contains("ITM")) isCanNextLevel = true;
            return isCanNextLevel;
        }
    }
}


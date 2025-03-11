using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DailyIntention
{
    public class IntentionSlot : MonoBehaviour
    {
        public IntentionDataSO currentDataSO;
        public Image bgIMG;
        public Image couldIMG;
        public Image textIMG;

        public void InitSlot(LevelManager levelManager,IntentionDataSO _dataSO)
        {
            this.currentDataSO = _dataSO;
            bgIMG.sprite = levelManager.intentionDatabaseSO.GetBGData(_dataSO.bGType).sprite;
            couldIMG.sprite = levelManager.intentionDatabaseSO.GetCloudData(_dataSO.cloudType).sprite;
            textIMG.sprite = _dataSO.txHeadIMG;
        }

        public void ClickButton()
        {
            GameManager.Instance.uIGameManager.IndexClick(currentDataSO);
            GameManager.Instance.uIGameManager.isFormAllCard = true;
        }
    }
}

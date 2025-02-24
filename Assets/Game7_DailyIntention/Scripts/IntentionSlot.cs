using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DailyIntention
{
    public class IntentionSlot : MonoBehaviour
    {
         public IntentionDataSO currentDataSO;

        public void ClickButton()
        {
            GameManager.Instance.uIGameManager.IndexClick(currentDataSO);
        }
    }
}

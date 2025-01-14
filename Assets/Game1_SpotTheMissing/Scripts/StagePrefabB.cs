using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SpotTheMissing
{
    public class StagePrefabB : MonoBehaviour
    {
        public RoundModelSO roundModelSO;
        private string hideID;
        public GameObject[] itmOBJ;

        //ซ่อนไอเทมที่ต้องหา
        public void HideItemIsCorrect()
        {
            hideID = roundModelSO.GetCorrectID(GameManager.Instance.levelManager.currentStage.id);
            itmOBJ.ToList().ForEach(o => {
                if(o.name == hideID) o.SetActive(false);
                else o.SetActive(true);
            });
        }
    }
}

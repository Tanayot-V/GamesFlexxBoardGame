using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DailyIntention
{
    public enum CloudType
    {
        cloud1,
        cloud2,
        cloud3,
        cloud4,
        cloud5,
        cloud6
    }

    [System.Serializable]
    public class CloudData
    {
        public CloudType cloudType;
        public Sprite sprite;
    }

    public enum BGType
    {
        BG1,
        BG2,
        BG3,
        BG4,
        BG5
    }
    [System.Serializable]
    public class BGData
    {
        public BGType bgType;
        public Sprite sprite;
    }

    [System.Serializable]
    public class PageData
    {
        public Sprite txHeadIMG;
        public Sprite txAnswerIMG;
        public CloudType cloudType;
        public BGType bGType;
    }
    
    public class LevelManager : MonoBehaviour
    {
        public IntentionDatabaseSO intentionDatabaseSO;
        public IntentionDataSO currentDataSO;
        public int currentIndex;

        public void RandomData()
        {
            currentDataSO = intentionDatabaseSO.GetRandomPage();
        }

        //เรียงลำดับ
        public void SetCurrentData(int index)
        {
            if (index >= 0 && index < intentionDatabaseSO.pageDatas.Length)
            {
                currentIndex = index;
                currentDataSO = intentionDatabaseSO.pageDatas[currentIndex];

                // ตัวอย่างการแสดงผล
                Debug.Log($"Current Data: {currentDataSO.name}, Index: {currentIndex}");
            }
            else
            {
                Debug.LogWarning("Index out of range!");
            }
        }

        public void NextData()
        {
            int nextIndex = (currentIndex + 1) % intentionDatabaseSO.pageDatas.Length;
            SetCurrentData(nextIndex);
        }
    }
}

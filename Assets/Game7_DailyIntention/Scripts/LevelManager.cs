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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DailyIntention
{
    [CreateAssetMenu(fileName = "DailyIntention", menuName = "DailyIntention/IntentionDataSO", order = 0)]
    public class IntentionDataSO : ScriptableObject
    {
       
        public Sprite txHeadIMG;
        public Sprite txAnswerIMG;
        public CloudType cloudType;
        public BGType bGType;
    }
}

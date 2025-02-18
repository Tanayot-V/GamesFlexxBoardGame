using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DailyIntention
{
    [CreateAssetMenu(fileName = "DailyIntentionDatabaseSO", menuName = "DailyIntention/DatabaseSO", order = 0)]

    public class IntentionDatabaseSO : ScriptableObject
    {
        public IntentionDataSO[] pageDatas;

        public CloudData[] cloudDatas;
        public BGData[] bgDatas;


        private Dictionary<CloudType,CloudData> cloudTypeDIC = new Dictionary<CloudType,CloudData>();
        public CloudData GetCloudData(CloudType _type)
        {
            if (cloudTypeDIC.ContainsKey(_type))
            {
                return cloudTypeDIC[_type];
            }
            else
            {
                return cloudTypeDIC[_type] = cloudDatas.ToList().Find(o => o.cloudType == _type);
            }
        }

        private Dictionary<BGType,BGData> bgTypeDIC = new Dictionary<BGType,BGData>();
        public BGData GetBGData(BGType _type)
        {
            if (bgTypeDIC.ContainsKey(_type))
            {
                return bgTypeDIC[_type];
            }
            else
            {
                return bgTypeDIC[_type] = bgDatas.ToList().Find(o => o.bgType == _type);
            }
        }

        public IntentionDataSO GetRandomPage()
        {
            int randomIndex = Random.Range(0, pageDatas.Length);
            return pageDatas[randomIndex];
        }
        
    }
}

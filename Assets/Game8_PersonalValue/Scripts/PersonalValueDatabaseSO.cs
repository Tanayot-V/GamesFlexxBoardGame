using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PersonalValue
{
    [CreateAssetMenu(fileName = "PersonalValueDatabaseSO", menuName = "PersonalValue/DatabaseSO", order = 0)]
    public class PersonalValueDatabaseSO : ScriptableObject 
    {
        public CardDataSO[] cardDataSO;
        public string[] boxsNameList_1;
        public string[] boxsNameList_2;
        public string[] boxsNameList_3;
        private Dictionary<string,CardDataSO> cardDataDIC = new Dictionary<string,CardDataSO>();
        public CardDataSO GetCardData(string _id)
        {
            if (cardDataDIC.ContainsKey(_id))
            {
                return cardDataDIC[_id];
            }
            else
            {
                CardDataSO foundDic = cardDataSO.ToList().Find(o => o != null && o.name == _id);
                if (foundDic == null)
                {
                    return default;
                }

                if (!string.IsNullOrEmpty(foundDic.name))
                {
                    cardDataDIC[_id] = foundDic;
                    return foundDic;
                }
                else
                {
                    Debug.LogError($"CardDataSO not found: {_id}");
                    return default;
                }
            }
        }
    }
}

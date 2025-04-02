using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalValue
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "PersonalValue/CardSO", order = 0)]
    public class CardDataSO : ScriptableObject
    {
        public string cardTH;
        public string cardEN;
        public Sprite picture;
    }
}

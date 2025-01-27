using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodWarShip
{
    [CreateAssetMenu(fileName = "CardDatabaseSO", menuName = "GodWarShip/CardDatabaseSO", order = 1)]
    public class CardDatabaseSO : ScriptableObject
    {
        [Header("Easy")]
        public CardSO[] level1Cards_easy;
        public CardSO[] level2Cards_easy;

        [Header("Normal&Hard")]
        public CardSO[] level1Cards;
        public CardSO[] level2Cards;
        public CardSO[] level3Cards;
        public CardSO[] level4Cards;

    }
}

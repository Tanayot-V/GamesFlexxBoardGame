using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodWarShip
{
    [CreateAssetMenu(fileName = "CardDatabaseSO", menuName = "GodWarShip/CardDatabaseSO", order = 1)]
    public class CardDatabaseSO : ScriptableObject
    {
        public CardSO[] level1Cards;
        public CardSO[] level2Cards;
        public CardSO[] level3Cards;
        public CardSO[] level4Cards;

        public CardSO[] GetCardLevel(LevelCard levelCard)
        {
            return levelCard switch
            {
                LevelCard.Level1 => level1Cards,
                LevelCard.Level2 => level2Cards,
                LevelCard.Level3 => level3Cards,
                LevelCard.Level4 => level4Cards,
                _ => null
            };
        }
    }
}

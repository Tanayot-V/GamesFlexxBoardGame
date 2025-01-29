using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodWarShip
{
    [CreateAssetMenu(fileName = "CardSO", menuName = "GodWarShip/CardSO", order = 1)]
    public class CardSO : ScriptableObject
    {
      public Sprite picture;
      public LevelCard level;
      public CardType type;
      public string description;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodWarShip
{
      [CreateAssetMenu(fileName = "CardSO", menuName = "GodWarShip/CardSO", order = 1)]
    public class CardSO : ScriptableObject
    {
      public string id;
      public Sprite picture;

      [Header("Mock up")]
      public string dispalyName;
      public string description;
      public Color textColor;
      public Color bgColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheMissing
{
[CreateAssetMenu(fileName = "RoundModelSO", menuName = "ScriptableObjects/RoundModelSO", order = 1)]
public class RoundModelSO : ScriptableObject
{
  public string id;
  public GameObject prefabA;
  public GameObject prefabB;
  public string correctID;
  public Sprite correctSprite;
}
}

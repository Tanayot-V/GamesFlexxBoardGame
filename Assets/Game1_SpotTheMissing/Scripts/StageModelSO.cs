using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheMissing
{
[CreateAssetMenu(fileName = "StageModelSO", menuName = "ScriptableObjects/StageModelSO", order = 1)]
public class StageModelSO : ScriptableObject
{
   public string id;
   public RoundModelSO[] roundModelSO;
}
}

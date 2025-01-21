using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game0 : MonoBehaviour
{
   public void ClickButton(string _name)
   {
    DataCenterManager.Instance.LoadSceneByName(_name);
   }
}

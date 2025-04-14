using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using BriefMe;

namespace PersonalValue
{
    public class UIController : MonoBehaviour
    {
        public GameManager gameManager;
        public void Start()
        {
            gameManager = GameManager.Instance;
        }

        #region Tutorial
        public void HideTutorialButton(string _buttonName)
        {
            switch (_buttonName)
            {
                case "1":
                    gameManager.tutorial.tutorialPageGroup.SetActive(false);
                    gameManager.levelManager.Stage1();
                    Debug.Log("Stage1");
                break;
            }
        }
        #endregion
    }
}

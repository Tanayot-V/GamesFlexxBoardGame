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

        public void EndStage4Button()
        {
            gameManager.levelManager.EndStage4();
        }
        public void EndTempleteButton()
        {
            gameManager.levelManager.EndTemplete();
        }

        public void MessageNextButton()
        {
            gameManager.levelManager.OpenTemplateInput();
        }

        public void MessageStopButton()
        {
            gameManager.levelManager.ShowMessage(14);
        }
    }
}

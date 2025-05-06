using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace PersonalValue
{
    public class UIManager : MonoBehaviour
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

        //คำถามไปต่อ
        public void MessageNextButton()
        {
            gameManager.levelManager.OpenTemplateInput();
        }

        //คำถาม พอแค่นี้
        public void MessageStopButton()
        {
            gameManager.levelManager.ShowMessage(14);
        }

        public void EndGameButton()
        {
            gameManager.levelManager.ShowMessage(15);
        }
    }
}

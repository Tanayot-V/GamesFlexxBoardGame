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
        public void MessageNextButton() //คุณต้องการลงลึกไปอีกไหม
        {
            //if( gameManager.levelManager.GetMessageIndex() == 13) gameManager.levelManager.OpenTemplateInput();
            if( gameManager.levelManager.GetMessageIndex() == 13) gameManager.levelManager.ShowMessage(16);
            if( gameManager.levelManager.GetMessageIndex() == 16) gameManager.levelManager.ShowMessage(17);
        }

        //คำถาม พอแค่นี้
        public void MessageStopButton() //คุณต้องการลงลึกไปอีกไหม
        {
            gameManager.levelManager.ShowMessage(14);//ไดอารอคตอนจบ
        }

        public void EndGameButton()
        {
            gameManager.levelManager.ShowMessage(15);
        }
    }
}

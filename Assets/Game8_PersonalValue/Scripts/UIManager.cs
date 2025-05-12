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
            if( gameManager.levelManager.GetMessageIndex() == 13) gameManager.levelManager.ShowMessage(17);
        }

        //คำถาม พอแค่นี้
        public void MessageStopButton() //คุณต้องการลงลึกไปอีกไหม
        {
            gameManager.levelManager.ShowMessage(21);//ไดอารอคตอนจบ
        }

        public void EndPriorityPageButton()
        {
            gameManager.levelManager.ShowMessage(9);
        } 
        
        public void EndLastPriorityPageButton()
        {
            gameManager.levelManager.ShowMessage(20);
        }
        
        public void ConfirmPageButton()
        {
            gameManager.levelManager.ConfirmPageButton();
        }

        public void OpenComfirmPageButton(GameObject _obj)
        {
          UiController.Instance.CanvasGroupFade(_obj,true,1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PersonalValue
{
    public class TextAnimation : MonoBehaviour
    {
        public void SetText1(string _text)
        {
            GameManager.Instance.levelManager.messageText[0].text = _text;
        }  
        public void SetText2(string _text)
        {
            GameManager.Instance.levelManager.messageText[1].text = _text;
        }

        public void SetImage(Sprite _sprite)
        {
            GameManager.Instance.levelManager.boxList[0].GetComponent<DropBox>().img.sprite = _sprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace BuildMe
{
    public class SetEventVideo : MonoBehaviour
    {
        public List<string> displayShow = new List<string>();
        public Image bgIMGTX;
        public GameObject itemGO;
        public GameObject parentGO;
        int index = 0;
        public TextMeshProUGUI[] itemTX;

        public void SetActiveVideo()
        {
            parentGO.SetActive(false);
        }

        public void SetImageActiveItemGO_F()
        {
            if (itemGO != null)
            {
                itemGO.SetActive(false);
            }
        }

        public void SetImageActiveItemGO_T()
        {
            if (itemGO != null)
            {
                itemGO.SetActive(true);
            }

            for(int i = 0; i < 4; i++)
            {
                Text(itemTX[i]);
                index++;
            }
        }
        
        public void Text(TextMeshProUGUI _text)
        {
            _text.text = displayShow[index];
            index++;
        }

        public void Init()
        {
            index = 0;
            parentGO.SetActive(true);
            SetImageActiveItemGO_F();
        }
    }
}

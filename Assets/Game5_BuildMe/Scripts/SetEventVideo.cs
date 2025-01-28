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
        public GameObject parentGO;
        int index = 0;
        public TextMeshProUGUI[] itemTX;

        public void SetActiveVideo()
        {
            parentGO.SetActive(false);
        }
        
        public void Text(TextMeshProUGUI _text)
        {
            _text.text = displayShow[index];
            index++;
        }

        public void AtTextAllLists()
        {
            itemTX.ToList().ForEach(x => Text(x));
        }

        public void Init()
        {
            index = 0;
            parentGO.SetActive(true);
        }
    }
}

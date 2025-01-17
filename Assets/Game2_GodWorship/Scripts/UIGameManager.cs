using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace GodWarShip
{
    public class UIGameManager : MonoBehaviour
    {
        public GameObject imgChilds;
        public GameObject cardSlot;
        public void SetAllIMG(List<CardSO> _cardSOs)
        {
            UiController.Instance.DestorySlot(imgChilds);
            int index = 0;
          _cardSOs.ForEach(o => {
            CardWorshipSlot cardWorshipSlot = UiController.Instance.InstantiateUIView(cardSlot,imgChilds).GetComponent<CardWorshipSlot>();
            cardWorshipSlot.pictureIMG.sprite = _cardSOs[index].picture;
            cardWorshipSlot.cardSO = o;
            cardWorshipSlot.gameObject.name = index.ToString();
            cardWorshipSlot.InitSlot(index);
            index++;
          });
        }
    }
}

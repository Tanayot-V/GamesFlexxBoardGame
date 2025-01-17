using System.Collections;
using System.Collections.Generic;
using GodWarShip;
using UnityEngine;
using UnityEngine.UI;

public class CardWorshipSlot : MonoBehaviour
{
    public int index;
    public CardSO cardSO;
    public bool isOpen;
    public Image pictureIMG;
    public GameObject coverGO;
    public TMPro.TextMeshProUGUI numberCardTX;

    public void InitSlot(int _index)
    {
        isOpen = false;
        coverGO.SetActive(true);
        this.GetComponent<Button>().targetGraphic = coverGO.GetComponent<Image>();
        index = _index;
        numberCardTX.text = (index + 1).ToString();
    }

    public void ClickButton()
    {
        if(!isOpen)
        {
            coverGO.GetComponent<CanvasGroupTransition>().FadeOut(()=>{
                coverGO.SetActive(false);
            });
            isOpen = true;
        }
        else
        {
            this.GetComponent<Button>().targetGraphic = pictureIMG;
        }
    }
}

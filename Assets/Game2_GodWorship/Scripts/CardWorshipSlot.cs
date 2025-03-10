using System.Collections;
using System.Collections.Generic;
using GodWarShip;
using UnityEngine;
using UnityEngine.UI;

namespace GodWarShip
{
public class CardWorshipSlot : MonoBehaviour
{
    public int index;
    public CardSO cardSO;
    public bool isOpen;
    public Image pictureIMG;
    public GameObject coverGO;
    public GameObject selectedAuraGO;
    public TMPro.TextMeshProUGUI numberCardTX;

    public void InitSlot(int _index)
    {
        isOpen = false;
        coverGO.SetActive(true);
        this.GetComponent<Button>().targetGraphic = coverGO.GetComponent<Image>();
        index = _index;
        numberCardTX.text = (index + 1).ToString();
        selectedAuraGO.SetActive(false);
        this.GetComponent<ButtonGroup>().key = name;
    }

    public void ClickButton()
    {
        if(!isOpen)
        {
            coverGO.GetComponent<CanvasGroupTransition>().FadeOut(()=>{
                coverGO.SetActive(false);
            });
            this.GetComponent<Button>().targetGraphic = pictureIMG;
            isOpen = true;
            GameManager.Instance.uIGameManager.OnButtonPressed(index);
            //Cover
            GameManager.Instance.uIGameManager.SetShowIMG(cardSO.picture);
            GameManager.Instance.uIGameManager.FaedShowCover();
            /*
            if(index <= 0)
            {
                GameManager.Instance.uIGameManager.FaedShowCover();
            }*/
        }
        else
        {
            GameManager.Instance.uIGameManager.SetShowIMG(cardSO.picture);
        }
        
        if(index >= (GameManager.Instance.uIGameManager.imgChilds.transform.childCount-1))
        {
            StartCoroutine(UiController.Instance.WaitForSecond(3,()=>{
                GameManager.Instance.uIGameManager.reloadGO.SetActive(true);
                //GameManager.Instance.uIGameManager.backGO.SetActive(false);
            }));
        }
    }
}
}

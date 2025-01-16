using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SpotTheMissing;

public class ButtonClickSlot : MonoBehaviour
{
    public GameObject[] auraOBJ;
    public UIBounceAnimation uIBounceAnimation;

    public void ShowAuraStage()
    {
        GameManager.Instance.uiGameManager.selecetStageButtons.ToList().ForEach(o => {o.GetComponent<ButtonClickSlot>().HideAuraStage();});
        auraOBJ.ToList().ForEach(o => {o.SetActive(true);});
        UITransition.Instance.ScaleOneSet(this.gameObject,Vector3.one,new Vector3(1.2f,1.2f,1.2f),0.5f);
        //if(uIBounceAnimation != null)uIBounceAnimation.StartBounce();
    }


    public void ShowAuraPlayer()
    {
        GameManager.Instance.uiGameManager.selecetPlayerButtons.ToList().ForEach(o => {o.GetComponent<ButtonClickSlot>().HideAuraStage();});
        auraOBJ.ToList().ForEach(o => {o.SetActive(true);});
        UITransition.Instance.ScaleOneSet(this.gameObject,Vector3.one,new Vector3(1.2f,1.2f,1.2f),0.5f);
        //if(uIBounceAnimation != null)uIBounceAnimation.StartBounce();
    }

    public void HideAuraStage()
    {
        auraOBJ.ToList().ForEach(o => {o.SetActive(false);});
       if(this.GetComponent<RectTransform>().localScale.x != 1f) UITransition.Instance.ScaleOneSet(this.gameObject,new Vector3(1.2f,1.2f,1.2f),Vector3.one,0.5f);
        //if(uIBounceAnimation != null)uIBounceAnimation.StopBounce();
    }
}

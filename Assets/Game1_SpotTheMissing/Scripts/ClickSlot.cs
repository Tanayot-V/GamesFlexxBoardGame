using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheMissing
{
public class ClickSlot : MonoBehaviour
{
    [SerializeField]private GameObject selectedObj;

    private void Start() {}

    public void SelectedObjHide()
    {
        selectedObj.SetActive(false);
    }
    private void SelectedObjShow()
    {
        selectedObj.SetActive(true);
    }

    private void OnMouseDown() {
        if(UiController.Instance.IsPointerOverUIObject()) return;
        GameManager.Instance.SetStageSummaryTOPrefab(this);
        GameManager.Instance.levelManager.currentStagePrefabA.ResetSelectedAll();
        GameManager.Instance.uiGameManager.OKButtonGreen();
        if(GameManager.Instance.IsLastStage()) GameManager.Instance.uiGameManager.okTX.text = "สรุปคะแนน";
        else GameManager.Instance.uiGameManager.okTX.text = "ยืนยัน";
        SelectedObjShow();
    }

    private void OnMouseOver() {
        this.GetComponent<SpriteRenderer>().color = Color.gray;
    }
 
    private void OnMouseExit() {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PersonalValue;

public class DropBox : MonoBehaviour
{
    public Image img;
    public TMPro.TextMeshProUGUI text;
    public string dropName;
    public List<CardDataSO> cardDataSOList = new List<CardDataSO>();
    public CardDataSO cardName_Stage4;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void UninstallCardStage4()
    {
        if (cardName_Stage4 != null)
        {
            GameManager.Instance.levelManager.UninstallCardStage4(this,cardName_Stage4);
            cardName_Stage4 = null;
        }
        else
        {
            Debug.Log("Card is null");
        }
    }

}

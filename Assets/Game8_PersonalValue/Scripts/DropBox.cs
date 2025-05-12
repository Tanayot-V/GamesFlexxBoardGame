using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PersonalValue;
using DG.Tweening;

public class DropBox : MonoBehaviour
{
    public Image img;
    public TMPro.TextMeshProUGUI text;
    public string dropName;
    public List<CardDataSO> cardDataSOList = new List<CardDataSO>();
    public CardDataSO cardName_Stage4;
    
}

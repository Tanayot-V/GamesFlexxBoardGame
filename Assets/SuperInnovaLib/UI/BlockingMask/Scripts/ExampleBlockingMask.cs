using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleBlockingMask : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;


    void Start()
    {
        BlockingMask.GetInstance().Show(btn1.gameObject);       
    }

    void Update()
    {
        
    }


    public void OnClickBtn1()
    {
        BlockingMask.GetInstance().Show(btn2.gameObject);
    }

    public void OnClickBtn2()
    {
        BlockingMask.GetInstance().Show(new Rect(btn3.transform.position, btn3.GetComponent<RectTransform>().sizeDelta * 1.2f), null);
    }

    public void OnClickBtn3()
    {
        BlockingMask.GetInstance().Show(btn1.gameObject);
    }
}

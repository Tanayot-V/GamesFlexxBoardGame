using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = Application.isMobilePlatform.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpotTheMissing
{
public class ClickSlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        this.GetComponent<SpriteRenderer>().color = Color.gray;
    }
 
    private void OnMouseExit() {
                this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropableItem : MonoBehaviour, IBeginDragHandler,IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isDefault = false;
    public Vector2 pos;
    public DraggableItem draggableItem;
    private Image childImage;
    public Color heightLightColor;
    public Color defaultColor;
    public Color disableColor;
    public GameObject heightLightItemObject;

    void Start()
    {
        childImage = this.GetComponent<Image>();
        defaultColor = childImage.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(name+ ": OnBeginDrag");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //Debug.Log(eventData.pointerDrag.name);
            //this.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = eventData.pointerDrag.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
            if(draggableItem == null)
            {
                draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
                if (draggableItem != null)
                {
                    if(draggableItem.isSnapOnSlot) return;
                    //สลับไอเทมไปที่ว่าง และเคลียร์ช่องเก่าให้ว่าง
                    if(draggableItem.parentDrop != null)
                    {
                        draggableItem.parentDrop.GetComponent<DropableItem>().draggableItem = null;
                    }
                     if(draggableItem.parentDrop == null)
                    {
                       Debug.Log("From UI");
                    }
                    draggableItem.SnapToSlot(this.gameObject);
                    draggableItem.transform.SetParent(transform);
                    draggableItem.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                Debug.Log("draggableItem(Switch!!):" + name);
                DraggableItem swicthableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
                GameObject parent = swicthableItem.parentDrop;
                if (swicthableItem != null)
                {
                    swicthableItem.SnapToSlot(this.gameObject);
                    swicthableItem.transform.SetParent(transform);
                    swicthableItem.transform.localPosition = Vector3.zero;
                }
                if (draggableItem != null)
                {
                    draggableItem.SnapToSlot(parent);
                    if(parent != null)draggableItem.transform.SetParent(parent.transform);
                    draggableItem.transform.localPosition = Vector3.zero;
                }
                swicthableItem.parentDrop.GetComponent<DropableItem>().draggableItem =  swicthableItem.parentDrop.transform.GetChild(0).GetComponent<DraggableItem>();
                if(parent != null)parent.GetComponent<DropableItem>().draggableItem = parent.transform.GetChild(0).GetComponent<DraggableItem>();
            }
        }
    }

    public void SetDropSlot(PointerEventData eventData)
    {
        draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggableItem != null)
        {
            if(draggableItem.isSnapOnSlot) return;
            draggableItem.SnapToSlot(this.gameObject);
            draggableItem.transform.SetParent(transform);
            draggableItem.transform.localPosition = Vector3.zero;
        }   
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
         if (eventData.pointerDrag == null) 
        {
            return;
        }
        // Change the color of the child image to indicate hover
        if (childImage != null)
        {
            if(draggableItem != null)
            {
                draggableItem.GetComponent<Image>().color = heightLightColor;
                //childImage.color = disableColor;// Change to your desired color
            }
            else
            {
                childImage.color = heightLightColor;// Change to your desired color
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert the color of the child image to the original color
        if (childImage != null)
        {
            childImage.color = defaultColor; // Change to the original color
        }
        if(draggableItem != null)
        {
            draggableItem.GetComponent<Image>().color = defaultColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

}

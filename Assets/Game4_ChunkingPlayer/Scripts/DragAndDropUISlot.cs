using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropUISlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TMPro.TextMeshProUGUI chunkingTX;
    public GameObject prefab;
    public Transform gameplayCanvas;
    private GameObject draggableItemObject;
    private DraggableItem draggableItem;

     public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag: " + name);
        //สร้าง Object
        //prefab = GameManager.Instance.CreatePipeSlotDragDropUI(pipeData);
        
        // Instantiate the DraggableItem prefab
        draggableItemObject = Instantiate(prefab, transform.parent);
        draggableItem = draggableItemObject.GetComponent<DraggableItem>();
        draggableItem.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = chunkingTX.text;
        draggableItemObject.transform.SetParent(gameplayCanvas);
        draggableItemObject.transform.localScale = new Vector3(1, 1, 1);

        // Set the initial position of the draggable item to the mouse position
        Transform rectTransform = draggableItemObject.GetComponent<Transform>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rectTransform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

        // Start dragging the item
        draggableItem.OnBeginDrag(eventData);
        eventData.pointerDrag = draggableItemObject;

        draggableItem.GetComponent<DraggableItem>().parentDrag = this.gameObject;
        draggableItem.name = transform.GetChild(0).transform.GetComponent<TMPro.TextMeshProUGUI>().text;
        this.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag: " + name);
        //Object ตามเมาส์
        if (draggableItem != null)
        {
            draggableItem.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag: " + name);
        //if (prefab != null) prefab.GetComponent<PipeSlotDragDrop>().OnMouseUpSlot();
        if (draggableItem != null)
        {
            draggableItem.OnEndDrag(eventData);
        }
    }
}

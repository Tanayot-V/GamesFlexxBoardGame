using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockingMask : MonoBehaviour
{
    private static Dictionary<Transform, BlockingMask> cachedCanvas = new Dictionary<Transform, BlockingMask>();

    public static BlockingMask GetInstance(Transform parent = null)
    {
        if (parent == null)
        {
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            parent = canvas.transform;
        }


        if (cachedCanvas.ContainsKey(parent))
        {
            if (cachedCanvas[parent] != null)
            {
                return cachedCanvas[parent];
            }
            else
            {
                cachedCanvas.Remove(parent);
            }
        }

        GameObject mask = new GameObject("[Blocking Mask] Masker");
        GameObject background = new GameObject("[Image] Background");

        mask.AddComponent<Image>().raycastTarget = false;
        mask.AddComponent<Mask>().showMaskGraphic = false;
        BlockingMask instance = mask.AddComponent<BlockingMask>();

        background.AddComponent<CanvasRenderer>();
        RectTransform backgroundRect = background.AddComponent<RectTransform>();
        InvertedMask image = background.AddComponent<InvertedMask>();
        image.raycastTarget = false;
        image.color = new Color(0, 0, 0, 0.5f);
        backgroundRect.sizeDelta = parent.GetComponent<RectTransform>().sizeDelta;
        backgroundRect.anchoredPosition = new Vector2(0, 0);

        mask.transform.SetParent(parent);
        backgroundRect.SetParent(mask.transform);

        mask.SetActive(false);

        cachedCanvas[parent] = instance;
        return instance;
    }


    private Image masker;
    private RectTransform maskerRect;
    private EventSystem eventSystem;
    private BaseInputModule baseInput;

    public bool isShowing = false;

    public void Awake()
    {
        masker     = this.GetComponent<Image>();
        maskerRect = masker.GetComponent<RectTransform>();
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        baseInput   = eventSystem.GetComponent<BaseInputModule>();
    }


    public void Show(GameObject target)
    {
        Image         targetImage = target.GetComponent<Image>();
        RectTransform targetRect  = target.GetComponent<RectTransform>();

        if (targetImage == null)
        {
            if (targetRect == null)
            {
                return;
            }

            Show(new Rect(targetRect.position, targetRect.sizeDelta));
            return;
        }

        masker.sprite        = targetImage.sprite;
        masker.type          = targetImage.type;
        maskerRect.position  = targetRect.position;
        maskerRect.sizeDelta = targetRect.sizeDelta;
        this.transform.localScale = target.transform.localScale;

        PerformShow();
    }


    public void Show(Rect rect, Sprite sprite = null, Image.Type imageType = Image.Type.Simple)
    {
        masker.sprite        = sprite;
        masker.type          = imageType;
        maskerRect.position  = rect.position;
        maskerRect.sizeDelta = rect.size;

        this.transform.localScale = Vector3.one;

        PerformShow();
    }


    public void Show(Vector2 anchorPosition, Vector2 size, Sprite sprite = null, Image.Type imageType = Image.Type.Simple)
    {
        masker.sprite               = sprite;
        masker.type                 = imageType;
        maskerRect.anchoredPosition = anchorPosition;
        maskerRect.sizeDelta        = size;

        this.transform.localScale = Vector3.one;

        PerformShow();
       
    }


    private void PerformShow()
    {
        isShowing            = true;

        Transform background = this.transform.GetChild(0);
        background.SetParent(this.transform.parent);

        background.transform.localScale = Vector3.one;
        background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        background.SetParent(this.transform);

        this.transform.SetAsLastSibling();
        this.gameObject.SetActive(true);
        eventSystem.enabled  = false;
        baseInput.enabled = false;
    }


    public void Hide()
    {
        this.gameObject.SetActive(false);
        eventSystem.enabled = true;
        isShowing = false;
    }


    void Update()
    {
        if (!isShowing)         return;
        //if (GetMouseButtton(0)) return;
        Vector2 localMousePosition; 
        RectTransformUtility.ScreenPointToLocalPointInRectangle(maskerRect, GetMousePosition(), null, out localMousePosition);

        if (maskerRect.rect.Contains(localMousePosition))
        {
            bool isEnable = eventSystem.enabled;
            eventSystem.enabled = true;
            baseInput.enabled = true;

            if (!isEnable)
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = GetMousePosition();

                List<RaycastResult> results = new List<RaycastResult>();
                eventSystem.RaycastAll(pointer, results);

                if (results.Count > 0)
                {
                    results.ForEach( o => Debug.Log(o.gameObject.name));
                    int index = results.FindIndex( o => o.gameObject.GetComponent<Button>() != null);

                    if (index != -1)
                    {
                        ExecuteEvents.Execute(results[0].gameObject, pointer, ExecuteEvents.pointerClickHandler);
                    }
                }
            }
        }
        else
        {
            eventSystem.enabled = false;
            baseInput.enabled = false;
        }
    }


    private bool GetMouseButtton(int index)
    {
#if ENABLE_INPUT_SYSTEM
        if (index == 0)
            return UnityEngine.InputSystem.Mouse.current.leftButton.isPressed;
        else if (index == 1)
            return UnityEngine.InputSystem.Mouse.current.rightButton.isPressed;
        else
            return UnityEngine.InputSystem.Mouse.current.middleButton.isPressed;
#else
        return Input.GetMouseButton(index);
#endif    
    }


   private Vector2 GetMousePosition()
    {
#if ENABLE_INPUT_SYSTEM
        return UnityEngine.InputSystem.Pointer.current.position.ReadValue();
       //return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
#else
      return Input.mousePosition;
#endif
    }
}

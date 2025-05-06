using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UiController : MonoBehaviour
{
    private static UiController _Instance;
    public static UiController Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject obj = new GameObject("[Script]: -- UiController Instance --");
                _Instance = obj.AddComponent<UiController>();
                DontDestroyOnLoad(obj);
            }
            return _Instance;
        }
    }

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult r in results)
        {
            if (r.gameObject.GetComponent<RectTransform>() != null)
                return true;
        }
        return false;
    }

     public bool IsPointerOverGameObjectWithTag(string _tag)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<RectTransform>() != null)
                {
                    if(result.gameObject.tag == _tag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    public void DestorySlot(GameObject tf)
    {
        if (tf == null)
        {
            Debug.LogWarning("DestorySlot is null");
            return;
        }

        if (tf.transform.childCount > 0)
        {
            for (int i = 0; i < tf.transform.childCount; i++)
            {
                Destroy(tf.transform.GetChild(i).gameObject);
            }
        }
    }

    public void HideChild(GameObject tf)
    {
        if (tf == null)
        {
            Debug.LogWarning("DestorySlot is null");
            return;
        }

        if (tf.transform.childCount > 0)
        {
            for (int i = 0; i < tf.transform.childCount; i++)
            {
                tf.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator WaitForSecond(float _seconds, System.Action callback = null)
    {
        yield return new WaitForSeconds(_seconds);

        callback?.Invoke();
    }

    public IEnumerator WaitForSecondRealtime(float seconds, System.Action callback)
    {
        yield return new WaitForSecondsRealtime(seconds);
        callback?.Invoke();
    }

    public Color SetColorWithHex(string _hexColor)
    {
        Color color;

        if (ColorUtility.TryParseHtmlString(_hexColor, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Unable to parse hex string to Color.");
        }
        return color;
    }

    public GameObject InstantiateUIView(GameObject _prefab,GameObject _parent = null)
    {
        GameObject prefab = Instantiate(_prefab);
        if(_parent != null) prefab.transform.SetParent(_parent.transform);
        prefab.GetComponent<RectTransform>().localScale = Vector3.one;
        
        RectTransform rect = prefab.GetComponent<RectTransform>();
        Vector3 pos = rect.localPosition;
        pos.z = 0f;
        rect.localPosition = pos;

        return prefab;
    }

    public void SetButtonGroup(string _group,string _key)
    {
        Button btn = ButtonGroupManager.Instance.GetButton(_group, _key);
        if (btn != null) ButtonGroupManager.Instance.Select(btn.GetComponent<ButtonGroup>());
    }

    public void CanvasGroupFade(GameObject _go,bool _IsFadeIn,float _duration,System.Action action = null)
    {
        CanvasGroup canvasGroup = _go.GetComponent<CanvasGroup>();
        //Fade In 0 => 1
        if(_IsFadeIn)
        {   
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1,_duration).OnComplete(()=>{
                action?.Invoke();
            });
        }

        //Fade Out 1 => 0
        else
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;
            canvasGroup.DOFade(0,_duration).OnComplete(()=>{
                canvasGroup.gameObject.SetActive(false); // ปิดหลัง fade out
                action?.Invoke();
            });        
        }
    }
    
}

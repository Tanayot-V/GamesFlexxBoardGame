using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIBounceAnimation : MonoBehaviour
{
     public RectTransform uiElement;
    public float scaleMultiplier = 1.5f;
    public float duration = 0.5f;
    public Ease animationEase = Ease.InOutQuad;

    private Tween bounceTween;

    [SerializeField] bool isPlayStart;

    void Start()
    {
        uiElement = this.GetComponent<RectTransform>();
        if(!isPlayStart)StartBounce();
    }

    public void StartBounce()
    {
        if (bounceTween != null && bounceTween.IsPlaying())
            bounceTween.Kill();

        bounceTween = uiElement.DOScale(Vector3.one * scaleMultiplier, duration)
            .SetEase(animationEase)
            .SetLoops(-1, LoopType.Yoyo)
            .SetUpdate(true);
    }

    public void StopBounce()
    {
        if (bounceTween != null)
        {
            bounceTween.Kill();
            uiElement.DOScale(Vector3.one, (duration/2)).SetEase(animationEase);
            Debug.Log(name+ "StopBounce");
        }
    }
}

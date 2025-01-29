using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public StoryCardManagar storyCardManagar;
    public int index;
    public MeshRenderer icon;
    public IconData data;
    public float flipTime = 1f;
    public bool isFlipping = false;
    public GameObject highlightParticle;
    public ParticleSystem ray,star;

    [Header("ShowCard")]
    public Vector3 originalPos;
    public bool isShow = false;
    public float showTime = 0.5f;

    public void SetUp(IconData _iconData, StoryCardManagar _storyCardManagar, int _index)
    {
        originalPos = transform.position;
        data = _iconData;
        icon.material.SetTexture("_BaseMap",_iconData.icon.texture);
        storyCardManagar = _storyCardManagar;
        index = _index;
    }

    public IEnumerator Highlight()
    {
        highlightParticle.SetActive(true);
        yield break;
    }

    public IEnumerator StopHighlight()
    {
        highlightParticle.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(3f);
        highlightParticle.SetActive(false);
    }

    public IEnumerator flip()
    {
        isFlipping = true;
        float t = 0;
        while (t < flipTime)
        {
            t += Time.deltaTime;
            transform.Rotate(Vector3.up, 180 * (Time.deltaTime / flipTime));
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 180, 0);
        ray.Stop();
        star.Stop();
        //highlightParticle.GetComponent<ParticleSystem>().Stop();
    }

    public void OnMouseDown()
    {
        if (!isFlipping || storyCardManagar.isShowResetGamePanel) return;
        if (!isShow)
        {
            StopAllCoroutines();
            StartCoroutine(ShowCard());
        } 
        else 
        {
            StopAllCoroutines();
            StartCoroutine(HideCard());
        }
    }

    public IEnumerator ShowCard()
    {
        storyCardManagar.ShowCard(index);
        isShow = true;
        float timer = 0;
        while (timer < showTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, storyCardManagar.cardShowPos.position, timer / showTime);
            yield return null;
        }
    }

    public IEnumerator HideCard()
    {
        storyCardManagar.showCardIndex = -1;
        isShow = false;
        float timer = 0;
        while (timer < showTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, originalPos, timer / showTime);
            yield return null;
        }
    }
}

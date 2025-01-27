using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer icon;
    public IconData data;
    public float flipTime = 1f;
    public bool isFlipping = false;
    public GameObject highlightParticle;
    public ParticleSystem ray,star;

    public void SetUp(IconData _iconData)
    {
        data = _iconData;
        icon.sprite = _iconData.icon;
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
}

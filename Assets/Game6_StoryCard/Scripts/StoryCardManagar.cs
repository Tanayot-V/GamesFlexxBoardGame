using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryCardManagar : MonoBehaviour
{
    public IconDataPool iconDataPool;
    public int OpenCardCount = 0;
    private int count;
    public List<Card> cards = new List<Card>();
    public List<int> openCardIndex = new List<int>();

    public int numberOfCardToOpen = 1;
    public Button plusButton, minusButton, openButton;
    public TMP_Text numberOfCardToOpenText;

    public Transform cardShowPos;

    public int showCardIndex = -1;

    [Space(50)]
    [Header("Transition")]
    public SpriteRenderer mainFade;
    public Animator openParticleAnim;
    public float animTime;

    [Space(30)]
    [Header("ResetGame")]
    public CanvasGroup resetGamePanel;
    public bool isShowResetGamePanel = false;

    
    void Start()
    {
        List<IconData> iconDatas = iconDataPool.GetRandomIconDatas(cards.Count);
        for (int i = 0; i < cards.Count; i++)
        {
            int temp = i;
            cards[i].SetUp(iconDatas[i], this, temp);
        }
        numberOfCardToOpen = 1;
        minusButton.interactable = false;
        numberOfCardToOpenText.text = numberOfCardToOpen.ToString();
        StartCoroutine(FadeStart());
    }

    private IEnumerator FadeStart()
    {
        while (mainFade.color.a > 0.01f)
        {
            mainFade.color -= new Color(0, 0, 0, 2f) * Time.deltaTime;
            yield return null;
        }
        mainFade.color = new Color(1f, 1f, 1f, 0f);
        yield return null;
    }

    public void PlusButton()
    {
        minusButton.interactable = true;
        numberOfCardToOpen++;
        if(numberOfCardToOpen + OpenCardCount == cards.Count)
        {
            plusButton.interactable = false;
        }
        numberOfCardToOpenText.text = numberOfCardToOpen.ToString();
    }

    public void MinusButton()
    {
        plusButton.interactable = true;
        if (numberOfCardToOpen > 1)
        {
            numberOfCardToOpen--;
            numberOfCardToOpenText.text = numberOfCardToOpen.ToString();
            if (numberOfCardToOpen == 1)
            {
                minusButton.interactable = false;
            }
        }
    }

    public void OpenCard()
    {
        if (showCardIndex != -1)
        {
            cards[showCardIndex].OnMouseDown();
        }
        openButton.interactable = false;

        for (int i = 0; i < openCardIndex.Count; i++)
        {
            if (cards[openCardIndex[i]].isFlipping)
            {
                StartCoroutine(cards[openCardIndex[i]].StopHighlight());
            }
        }

        count = numberOfCardToOpen;
        OpenCardCount += count;
        
        StartCoroutine(OpenCardCo());
    }

    public IEnumerator OpenCardCo()
    {
        while (mainFade.color.a < 1f)
        {
            mainFade.color += new Color(0, 0, 0, 3f) * Time.deltaTime;
            yield return null;
        }
        mainFade.color = Color.white;

        openParticleAnim.SetTrigger("Play");
        yield return new WaitForSeconds(animTime);

        while (mainFade.color.a > 0.01f)
        {
            mainFade.color -= new Color(0, 0, 0, 2f) * Time.deltaTime;
            yield return null;
        }
        mainFade.color = new Color(1f, 1f, 1f, 0f);
        yield return null;

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, cards.Count);
            if (openCardIndex.Contains(randomIndex))
            {
                i--;
                continue;
            }
            openCardIndex.Add(randomIndex);
            StartCoroutine(cards[randomIndex].Highlight());
        }
        StartCoroutine(ShowCard());
    }

    public IEnumerator ShowCard()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < openCardIndex.Count; i++)
        {
            if(cards[openCardIndex[i]].isFlipping)
            {
                continue;
            }
            StartCoroutine(cards[openCardIndex[i]].flip());
            yield return new WaitForSeconds(1f);
        }
        openButton.interactable = true;
        numberOfCardToOpen = 1;
        minusButton.interactable = false;
        numberOfCardToOpenText.text = numberOfCardToOpen.ToString();
        if(OpenCardCount == cards.Count)
        {
            plusButton.interactable = false;
            minusButton.interactable = false;
            openButton.interactable = false;
        }
    }

    public void ResetGameBtnClick()
    {
        if (showCardIndex != -1)
        {
            cards[showCardIndex].OnMouseDown();
        }
        isShowResetGamePanel = true;
        StartCoroutine(FadeResetGamePanel(true));
    }
    public void CancelResetGameBtnClick()
    {
        isShowResetGamePanel = false;
        StartCoroutine(FadeResetGamePanel(false));
    }
    public void ConfirmResetGameBtnClick()
    {
        StartCoroutine(LoadSceneReset());
    }

    private IEnumerator LoadSceneReset()
    {
        while (mainFade.color.a < 1f)
        {
            mainFade.color += new Color(0, 0, 0, 3f) * Time.deltaTime;
            yield return null;
        }
        mainFade.color = Color.white;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator FadeResetGamePanel(bool fadein)
    {
        if(fadein)
        {
            while (resetGamePanel.alpha < 1f)
            {
                resetGamePanel.alpha += 0.01f;
                yield return null;
            }
            resetGamePanel.alpha = 1;
            resetGamePanel.interactable = true;
            resetGamePanel.blocksRaycasts = true;
        }
        else
        {
            while (resetGamePanel.alpha > 0.01f)
            {
                resetGamePanel.alpha -= 0.01f;
                yield return null;
            }
            resetGamePanel.alpha = 0;
            resetGamePanel.interactable = false;
            resetGamePanel.blocksRaycasts = false;
        }
        
    }

    public void ShowCard(int index)
    {
        if (showCardIndex != -1)
        {
            cards[showCardIndex].OnMouseDown();
        }
        showCardIndex = index;
    }
}

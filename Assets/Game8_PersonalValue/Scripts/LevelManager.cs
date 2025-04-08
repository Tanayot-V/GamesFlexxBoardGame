using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;

namespace PersonalValue
{    
    [System.Serializable]
    public enum Stage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }
    [System.Serializable]
    public class BoxData
    {
        public string boxName;
        public Sprite sprite;
    }

    public class LevelManager : MonoBehaviour
    {
        [Header("MainGame")]
        public GameObject messagePages;
        public GameObject messageGroup;        
        public GameObject messageButton;
        public GameObject[] stageCardPages;
        public GameObject[] stageCardPriority;
        public GameObject[] stageTemplate;

        [Header("Stage Model")]
        public Camera mainCamera;
        public GameObject canvasGame;
        public TMPro.TextMeshProUGUI headerText;
        public TMPro.TextMeshProUGUI messageText;
        public TMPro.TextMeshProUGUI fillText;
        public Image bgIMG;
        public GameObject fillBar;
        public GameObject boxPrefab;
        public GameObject boxParent;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public GameObject mockUpDragCard;
        private readonly int maxCardCount = 5;

        [Header("Stage State")]
        private int messageIndex = 0;
        public int fillCardCountCurrent = 0;
        public int fillCardCountMax = 0;
        public int currentCardCount = 0;
        public Stage currentStage;
        public DropBox importantBOX;
        public List<GameObject> boxList = new List<GameObject>();
        private List<CardDataSO> cardDataList = new List<CardDataSO>();

        #region Message
        public void ShowMessage(int _index)
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(false); });

            messagePages.GetComponent<CanvasGroup>().alpha = 1;
            messageIndex = _index;
            messageText.text = GameManager.Instance.cardDatabaseSO.messages[_index];

            messageButton.GetComponent<Button>().interactable = true;

            /*
            // ตั้งค่าเริ่มต้นให้โปร่งใสก่อน
            Color startColor = messageText.color;
            startColor.a = 0;
            messageText.color = startColor;

            // 🔥 เฟดเข้า (0 → 1) ใน 0.5 วินาที
            messageText.DOFade(1f, 2f).OnComplete(() =>
            {
                Debug.Log("ข้อความหายแล้ว! ✅");
                messageButton.SetActive(true);
                // หรือจะ messageText.gameObject.SetActive(false); ก็ได้
            });*/
        }

        public void ButtonMessage()
        {
            messageButton.GetComponent<Button>().interactable = false;
            messageText.GetComponent<TMPro.TextMeshProUGUI>().color = UiController.Instance.SetColorWithHex("#000000");

            CanvasGroup cg = messagePages.GetComponent<CanvasGroup>();
            cg.alpha = 1; // เริ่มโปร่งใส
            cg.DOFade(0f, 0.75f) // ค่อยๆ แสดง
            .SetEase(Ease.InOutSine)// เลือก Ease ให้รู้สึก smooth
            .OnComplete(()=> { 
                messagePages.GetComponent<CanvasGroup>().alpha = 0;
                messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;
                stageCardPages.ToList().ForEach(o => { o.SetActive(true); });
                switch (messageIndex)
                {
                    case 0:
                        Stage1();
                        break;
                    case 1:
                        Stage2();
                        break;
                    case 2:
                        Stage3();
                        break;
                    case 3:
                        Stage4();
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
                });
        }
        #endregion

        #region Gameplay
        public void Start()
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.mobileKeyboardSupport = true; //ต้องมีไม่งั้นไอแพดเปิดแป้นพิมพ์ไม่ได้
            #endif
            ShowMessage(0);
        }

        public void Stage1()
        {
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1092);
            currentStage = Stage.Stage1;
            headerText.text = GameManager.Instance.cardDatabaseSO.headers[0];
            GameManager.Instance.countdownTimer.StartCountdown();
            PersonalValueDatabaseSO databaseSO = GameManager.Instance.cardDatabaseSO;
            fillCardCountMax = databaseSO.cardDataSO.Count();
            fillCardCountCurrent = 0;
            UpdateFillCount(0);
            //Create Cards
            databaseSO.cardDataSO.ToList().ForEach(o =>
            {
                if (o != null)
                {
                    cardDataList.Add(o);
                }
            });

            //Create Box
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(300, 300);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(30, 0);
            CreateBOX(databaseSO.boxsNameList_1);
            RerollCard();
      }
      
        private void Stage2()
        {
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 354);
            currentStage = Stage.Stage2;
            headerText.text = GameManager.Instance.cardDatabaseSO.headers[1];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(325, 325);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(170, 0);

            NextStage();
            InitStage(GameManager.Instance.cardDatabaseSO.boxsNameList_2);
        }

        private void Stage3()
        {
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
            currentStage = Stage.Stage3;
            headerText.text = GameManager.Instance.cardDatabaseSO.headers[2];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 350);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(90, 0);

            NextStage();
            InitStage(GameManager.Instance.cardDatabaseSO.boxsNameList_3);
        }

         private void Stage4()
        {
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2162);
            currentStage = Stage.Stage4;
            headerText.text = GameManager.Instance.cardDatabaseSO.headers[2];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(310, 310);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(30, 0);

            CreateBOX(GameManager.Instance.cardDatabaseSO.boxsNameList_4);
        }

        private void InitStage(BoxData[] _nameBox)
        {
            GameManager.Instance.countdownTimer.StartCountdown();
            CreateBOX(_nameBox);
            RerollCard();
        }

        private void CreateBOX(BoxData[] nameBOX)
        {
            UiController.Instance.DestorySlot(boxParent);
            int index = 0;
            boxList.Clear();
            nameBOX.ToList().ForEach(o =>
            {
                GameObject box = UiController.Instance.InstantiateUIView(boxPrefab ,boxParent);
                box.name = "boxStage_" + index;
                box.GetComponent<DropBox>().dropName = o.boxName;
                if(currentStage == Stage.Stage4) box.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
                else box.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = o.boxName;   
                box.GetComponent<Image>().sprite = o.sprite;


                if (o != null)
                {
                    boxList.Add(box);
                }
                if(index == 0)
                {
                    importantBOX = box.GetComponent<DropBox>();
                }
                index++;

                // 🔥 เพิ่ม Effect Scale ด้วย DOTween
                box.transform.localScale = Vector3.zero; // เริ่มจาก 0
                box.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up

            });
        }

        public void RerollCard()
        {
            GameManager.Instance.countdownTimer.StartCountdown();
            Shuffle(cardDataList);
            UiController.Instance.DestorySlot(cardParent);

            int loopCount = Mathf.Min(maxCardCount, cardDataList.Count);
            for(int i = 0; i < loopCount; i++)
            {
                GameObject card = UiController.Instance.InstantiateUIView(cardPrefab ,cardParent);
                card.name = cardDataList[i].name;
                card.GetComponent<Image>().sprite = cardDataList[i].picture;
                card.GetComponent<DragDropCard>().cardDataSO = cardDataList[i];

                 // 🔥 เพิ่ม Effect Scale ด้วย DOTween
                card.transform.localScale = Vector3.zero; // เริ่มจาก 0
                card.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up
            }
            currentCardCount = loopCount;
            mockUpDragCard.SetActive(false);
        }

        private void Shuffle(List<CardDataSO> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int rand = Random.Range(i, list.Count);
                CardDataSO temp = list[i];
                list[i] = list[rand];
                list[rand] = temp;
            }

            Debug.Log("✅ สุ่มการ์ดเสร็จแล้ว");
        }

        public void RemoveCardFromList(CardDataSO cardDataSO)
        {
            cardDataList.Remove(cardDataSO);
            //Debug.Log("🟢 ลบการ์ด " + cardDataSO.name + " ออกจากรายการการ์ด");
        }

        public bool IsLastCard()
        {
            if (cardParent.transform.childCount <= 0)
            {
                Debug.Log("🟢 การ์ดหมดแล้ว");
                return true;
            }
            else
            {
                Debug.Log("🟢 ยังมีการ์ดเหลืออยู่");
                return false;
            }
        }

        public void CheckCardCount()
        {
            if (currentCardCount <= 0)
            {
                Debug.Log("🟢 การ์ดหมดแล้ว");
                RerollCard();
            }
        }

        public bool CheckAllCardCount()
        {
            if (cardDataList.Count <= 0)
            {
                switch(currentStage)
                {
                    case Stage.Stage1:

                    stageCardPages[0].SetActive(false);
                    StartCoroutine(PlayAnimationThen("CameraZoom", () =>
                    {
                        messagePages.GetComponent<CanvasGroup>().alpha = 1;
                        messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        ShowMessage(1);

                        canvasGame.GetComponent<Animator>().Play("Message_2");
                    }));

                        break;
                    case Stage.Stage2:
                        messagePages.GetComponent<CanvasGroup>().alpha = 1;
                        messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        canvasGame.GetComponent<Animator>().Play("Message_3");
                        //messagePages.GetComponent<Animator>().SetInteger("messIndex",2);
                        ShowMessage(2);
                        break;
                    case Stage.Stage3:
                        //เช็คว่า ขาดไม่ได้ จะต้องไม่เกิน 15 ใบ
                        if(importantBOX.cardDataSOList.Count > 15)
                        {
                            Debug.Log("🟢 ขาดไม่ได้ เกิน 15 ใบ เล่นใหม่");
                            Stage3();
                        }
                        else
                        {
                            Debug.Log("🟢 ขาดไม่ได้ ไม่เกิน 15 ใบ ไปด่านต่อไป");
                            currentStage = Stage.Stage4;
                            canvasGame.GetComponent<Animator>().Play("Message_4");
                            ShowMessage(3);
                        }
                        
                        break;
                    case Stage.Stage4:
                        currentStage = Stage.Stage5;
                        break;
                    case Stage.Stage5:
                        currentStage = Stage.Stage1;
                        break;
                }
                    
                Debug.Log("🟢 ด่านต่อไป");
                return true;
            }
            else
            {
                Debug.Log("🟢 ยังมีการ์ดเหลืออยู่");
                return false;
            }
        }

        private void NextStage()
        {
            cardDataList.Clear();
            importantBOX.cardDataSOList.ToList().ForEach(o =>
            {
                cardDataList.Add(o);
            });
            fillCardCountMax = cardDataList.Count();
            fillCardCountCurrent = 0;
            UpdateFillCount(0);
        }

        public void UpdateFillCount(int _fillCount)
        {
            fillCardCountCurrent += _fillCount;
            fillText.text = "สำเร็จแล้ว " + fillCardCountCurrent + "/" + fillCardCountMax;
            fillBar.GetComponent<Image>().fillAmount = (float)fillCardCountCurrent / fillCardCountMax;
        }


        private IEnumerator PlayAnimationThen(string animationName, System.Action onComplete)
        {
            Animator animator = canvasGame.GetComponent<Animator>();
            animator.Play(animationName);

            // รอจนกว่าอนิเมชั่นจะหยุด
            yield return null;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);

            onComplete?.Invoke();
        }
      #endregion
    }
}

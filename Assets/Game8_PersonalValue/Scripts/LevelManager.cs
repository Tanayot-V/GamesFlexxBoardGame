using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

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
        private GameManager gameManager;
        public GameObject messagePages;
        public GameObject messageGroup;        
        public GameObject messageButton;
        public GameObject[] stageCardPages;
        public GameObject[] stageCardPriority;
        public GameObject[] stageTemplate;
        public GameObject[] stageTemplateInput;
        public GameObject[] tutorialPages;
        public GameObject cropImagePage;

        [Header("Stage Model")]
        public Camera mainCamera;
        public GameObject canvasGame;
        public Sprite nullSprite;
        public TMPro.TextMeshProUGUI headerText;
        public TMPro.TextMeshProUGUI[] messageText;
        public Image bgIMG;
        public GameObject fillBarGroup;
        public TMPro.TextMeshProUGUI fillText;
        public GameObject fillBar;
        public GameObject boxPrefab;
        public GameObject boxPriorityPrefab;
        public GameObject boxParent;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public GameObject mockUpDragCard;
        public GameObject timeObj;
        public GameObject priorityParent;
        public GameObject cameraZoomIMG;
        private readonly int maxCardCount = 5;

        [Header("Stage State")]
        public int fillCardCountCurrent = 0;
        private int messageIndex = 0;
        public int fillCardCountMax = 0;
        public int currentCardCount = 0;
        public Stage currentStage;
        public DropBox importantBOX;
        public List<GameObject> boxList = new List<GameObject>();
        public List<GameObject> priorityList = new List<GameObject>();
        private List<CardDataSO> cardDataList = new List<CardDataSO>();


        #region Message
        public void ShowMessage(int _index)
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(false); });
            cropImagePage.SetActive(false);
            GameManager.Instance.tutorial.tutorialPageGroup.SetActive(false);
            cameraZoomIMG.SetActive(false);

            messagePages.GetComponent<CanvasGroup>().alpha = 1;
            messageIndex = _index;

            messageButton.GetComponent<Button>().interactable = true;
            canvasGame.GetComponent<Animator>().Play("Message_0",0,0);

            //Show Text Message ว่าอะไร
            switch(_index)
            {
                case 0:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[0];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[1];
                    break;
                case 1:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[2];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[3];
                    break;
                case 2:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[4];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[5];
                    break;
                case 3:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[6];
                    messageText[1].text = string.Empty;
                    break;
            }
        }

        //หลังโชว์เสร็จกดแล้วจะไปไหน
        public void ButtonMessage()
        {
            messageButton.GetComponent<Button>().interactable = false;

            CanvasGroup cg = messagePages.GetComponent<CanvasGroup>();
            cg.alpha = 1; // เริ่มโปร่งใส
            cg.DOFade(0f, 0.75f) // ค่อยๆ แสดง
            .SetEase(Ease.InOutSine)// เลือก Ease ให้รู้สึก smooth
            .OnComplete(()=> {
                //messageButton.GetComponent<Button>().interactable = true; 
                messagePages.GetComponent<CanvasGroup>().alpha = 0;
                stageCardPages[1].SetActive(true);
                switch (messageIndex)
                {
                    case 0: ShowMessage(1); break;
                    case 1: ShowMessage(2); break;
                    case 2: ShowMessage(3); break;
                    case 3:
                    //เลื่อนขึ้น และ เข้า Tutorial
                        messagePages.GetComponent<CanvasGroup>().alpha = 1;
                        StartCoroutine(PlayAnimationThen("BGTransition_1", () =>{
                        messageButton.GetComponent<Button>().interactable = true;

                        messagePages.GetComponent<CanvasGroup>().alpha = 0;
                        messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        //Open Tutorial
                        gameManager.tutorial.tutorialPageGroup.SetActive(true);
                        canvasGame.GetComponent<Animator>().Play("Tutorial");
                          }));
                        break;
                    case 4: Stage2();break;
                    case 5: Stage3();break;
                    default:
                        break;
                }
                });

                Debug.Log("🟢 กดปุ่มแล้ว" + messageIndex);
        }
        #endregion

        #region Gameplay
        public void Start()
        {
            gameManager = GameManager.Instance;
            #if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.mobileKeyboardSupport = true; //ต้องมีไม่งั้นไอแพดเปิดแป้นพิมพ์ไม่ได้
            #endif
            ShowMessage(2);
        }

        public void Stage1()
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); });
            gameManager.tutorial.tutorialPageGroup.SetActive(false);
            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;

            fillBarGroup.SetActive(true);
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1092);
            currentStage = Stage.Stage1;
            headerText.text = GameManager.Instance.cardDatabaseSO.headers[0];
            PersonalValueDatabaseSO databaseSO = gameManager.cardDatabaseSO;
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
            CreateBOX(databaseSO.boxsNameList_1,boxPrefab);
            RerollCard();
      }
      
        private void Stage2()
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); });
            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;
            fillBarGroup.SetActive(true);

            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 354);
            currentStage = Stage.Stage2;
            headerText.text = gameManager.cardDatabaseSO.headers[1];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(325, 325);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(170, 0);

            timeObj.SetActive(true);
            NextStage();
            InitStage(gameManager.cardDatabaseSO.boxsNameList_2,boxPrefab);
        }

        private void Stage3()
        {
            fillBarGroup.SetActive(true);
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
            currentStage = Stage.Stage3;
            headerText.text = gameManager.cardDatabaseSO.headers[2];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 350);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(90, 0);

            timeObj.SetActive(true);
            NextStage();
            InitStage(gameManager.cardDatabaseSO.boxsNameList_3 ,boxPrefab);
        }

         private void Stage4()
        {
            fillBarGroup.SetActive(true);
            stageCardPriority.ToList().ForEach(o => { o.SetActive(true); });
            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2162);
            currentStage = Stage.Stage4;
            headerText.text = gameManager.cardDatabaseSO.headers[3];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 350);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);

            gameManager.countdownTimer.StopCountdown();
            timeObj.SetActive(false);
            NextStage();
            CreateBOX(gameManager.cardDatabaseSO.boxsNameList_4, boxPriorityPrefab);
            
            UiController.Instance.DestorySlot(priorityParent);
            cardDataList.ToList().ForEach(o =>
            {
                GameObject card = UiController.Instance.InstantiateUIView(cardPrefab, priorityParent);
                card.name = o.name;
                card.GetComponent<Image>().sprite = o.picture;
                card.GetComponent<DragDropCard>().cardDataSO = o;

                 // 🔥 เพิ่ม Effect Scale ด้วย DOTween
                card.transform.localScale = Vector3.zero; // เริ่มจาก 0
                card.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up
            });
            
            Debug.Log("Stage4_2");
        }

        private void InitStage(BoxData[] _nameBox ,GameObject _boxPrefab)
        {
            gameManager.countdownTimer.StartCountdown();
            CreateBOX(_nameBox,_boxPrefab);
            RerollCard();
        }

        private void CreateBOX(BoxData[] _nameBOX ,GameObject _boxPrefab)
        {
            UiController.Instance.DestorySlot(boxParent);
            int index = 0;
            boxList.Clear();
            Debug.Log(_nameBOX.Length);
            _nameBOX.ToList().ForEach(o =>
            {
                GameObject box = UiController.Instance.InstantiateUIView(_boxPrefab ,boxParent);
                box.name = "boxStage_" + index;
                box.GetComponent<DropBox>().dropName = o.boxName;
                if(currentStage == Stage.Stage4) 
                {
                    box.transform.GetChild(0).GetComponent<Image>().sprite = nullSprite;
                }
                else 
                {
                    box.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = o.boxName;
                }   
                box.GetComponent<Image>().sprite = o.sprite;

                if (o != null)
                {
                    boxList.Add(box);
                }
                if(index == 0 && currentStage != Stage.Stage4)
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
            Shuffle(cardDataList);
            UiController.Instance.DestorySlot(cardParent);

            int loopCount = Mathf.Min(maxCardCount, cardDataList.Count);
            for(int i = 0; i < loopCount; i++)
            {
               CreateCard(cardPrefab,cardParent,cardDataList[i]);
            }
            currentCardCount = loopCount;
            mockUpDragCard.SetActive(false);
            gameManager.countdownTimer.StartCountdown();
        }

        public void CreateCard(GameObject _prefab, GameObject _parent,CardDataSO _cardDataSO)
        {
            GameObject card = UiController.Instance.InstantiateUIView(_prefab ,_parent);
            card.name = _cardDataSO.name;
            card.GetComponent<Image>().sprite = _cardDataSO.picture;
            card.GetComponent<DragDropCard>().cardDataSO = _cardDataSO;

                // 🔥 เพิ่ม Effect Scale ด้วย DOTween
            card.transform.localScale = Vector3.zero; // เริ่มจาก 0
            card.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up
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
                    //หลังจบด่าน 1
                    case Stage.Stage1:
                    stageCardPages[0].SetActive(false);
                    boxList.ForEach(o => { o.GetComponent<CanvasGroup>().alpha = 0; });
                    cameraZoomIMG.SetActive(true);
                    cameraZoomIMG.GetComponent<Image>().sprite = importantBOX.GetComponent<Image>().sprite;
                    StartCoroutine(PlayAnimationThen("CameraZoom_1", () =>
                    {
                        cameraZoomIMG.SetActive(false);

                        messagePages.GetComponent<CanvasGroup>().alpha = 1;
                        messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;

                        StartCoroutine(PlayAnimationThen("BGTransition_2", () =>
                        {
                            messageIndex = 4;
                            ButtonMessage();//=> ด่าน 2 
                        }));
                    }));
                        break;
                    case Stage.Stage2:
                        stageCardPages[0].SetActive(false);
                        boxList.ForEach(o => { o.GetComponent<CanvasGroup>().alpha = 0; });
                        cameraZoomIMG.SetActive(true);
                        cameraZoomIMG.GetComponent<Image>().sprite = importantBOX.GetComponent<Image>().sprite;
                        StartCoroutine(PlayAnimationThen("CameraZoom_1", () =>
                        {
                            cameraZoomIMG.SetActive(false);

                            messagePages.GetComponent<CanvasGroup>().alpha = 1;
                            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;

                            StartCoroutine(PlayAnimationThen("BGTransition_3", () =>
                            {
                                messageIndex = 5;
                                ButtonMessage();//=> ด่าน 3 
                            }));

                        }));
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
                            messagePages.GetComponent<CanvasGroup>().alpha = 1;
                            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
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

         public void UpdateFillCount_Stage4()
        {
            fillCardCountMax = cardDataList.Count;
            if(fillCardCountMax > 5) fillCardCountMax = 5;
            fillCardCountCurrent = boxStageCount();
            fillText.text = "สำเร็จแล้ว " + boxStageCount() + "/" + fillCardCountMax;
            fillBar.GetComponent<Image>().fillAmount = (float)fillCardCountCurrent / fillCardCountMax;
        }

        private IEnumerator PlayAnimationThen(string animationName, System.Action onComplete)
        {
            Animator animator = canvasGame.GetComponent<Animator>();
            animator.Play(animationName,0,0);

            // รอจนกว่าอนิเมชั่นจะหยุด
            yield return null;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);

            onComplete?.Invoke();
        }

        //นับจำนวนไพ่ที่อยู่ในกล่องแล้ว ใน stage4
        public int boxStageCount()
        {
            int count = 0;
            boxList.ToList().ForEach(o =>
            {
               if( o.GetComponent<DropBox>().cardName_Stage4 != null)
                {
                    count++;
                }
            });
            return count;
        }

        public void OpenTemplate()
        {
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(true); });

            CanvasGroup canvasGroup = stageTemplate[0].GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, 0.5f).OnComplete(()=>{
                
            });

            int index = 0;
            boxList.ToList().ForEach(o =>
            {
                if(o.GetComponent<DropBox>().cardName_Stage4 != null)
                {
                    priorityList[index].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = o.GetComponent<DropBox>().cardName_Stage4.cardTH;
                }
                else
                {
                    priorityList[index].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
                }
                index++;
            });
        }
      #endregion
    }
}

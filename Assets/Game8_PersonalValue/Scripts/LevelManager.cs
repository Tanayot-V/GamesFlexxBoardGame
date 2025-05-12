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
        Template,
        TemplateInput
    }
    [System.Serializable]
    public class BoxData
    {
        public string boxName;
        public Sprite sprite;
    }
    [System.Serializable]
    public class TextAminTemplate
    {
        public string[] strings;
    }

    [System.Serializable]
    public class MessageAnimTemplate
    {
        public string text_1;
        public string text_2;
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
        public GameObject[] animationTemplatePage;
        public GameObject cropImagePage;

        [Header("Stage Model")]
        public Camera mainCamera;
        public Animator canvasAnim;
        public TMPro.TextMeshProUGUI headerText;
        public TMPro.TextMeshProUGUI[] messageText;
        public Image bgIMG;
        public Image bgIMGMessage;
        public GameObject fillBarGroup;
        public TMPro.TextMeshProUGUI fillText;
        public GameObject fillBar;
        public GameObject boxPrefab;
        public GameObject boxPriorityPrefab;
        public GameObject boxParent;
        public GameObject cardPrefab;
        public GameObject cardParent;
        public GameObject mockUpDragCard;
        public GameObject mockUpDragCardUn;
        public GameObject timeObj;
        public Button myValueButton;
        public GameObject priorityParent;
        public TMPro.TMP_InputField nameTextField_Stage4;
        public TMPro.TMP_InputField nameTextField_Stage5;
        public GameObject cameraZoomGroup;
        public GameObject cameraZoomIMG;
        public GameObject[] cameraZoomMockIMG;
        private readonly int maxCardCount = 5;
        public TMPro.TextMeshProUGUI mackupAminTemplateTX;


        [Header("Stage State")]
        public int fillCardCountCurrent = 0;
        private int messageIndex = 0;
        public int fillCardCountMax = 0;
        public int currentCardCount = 0;
        private int confirmPageIndex = 0;
        private int boxAnimateIndex = 0;
        public Stage currentStage;
        public DropBox importantBOX;
        public List<GameObject> boxList = new List<GameObject>();
        public List<TMPro.TextMeshProUGUI> priorityListTexts = new List<TMPro.TextMeshProUGUI>();
        public List<TMPro.TextMeshProUGUI> priorityLastListTexts  = new List<TMPro.TextMeshProUGUI>();
        public List<TMPro.TextMeshProUGUI> priorityLastSummaryListTexts = new List<TMPro.TextMeshProUGUI>();
        public List<GameObject> templateBoxList = new List<GameObject>();
        private List<string> templateBoxSummaryList = new List<string>();
        private List<CardDataSO> cardDataList = new List<CardDataSO>();
        private List<CardDataSO> cardDataList_Stage1 = new List<CardDataSO>(); //ไพ่ด่านที่ 1
        private List<CardDataSO> cardDataList_Stage2 = new List<CardDataSO>(); //ไพ่ด่านที่ 2
        private List<CardDataSO> cardDataList_Stage3 = new List<CardDataSO>(); //ไพ่ด่านที่ 3
        private List<MessageAnimTemplate> messageAnim = new List<MessageAnimTemplate>();
        private bool hasStoppedBlink_6 = false;
        private bool hasStoppedBlink_5 = false;


        #region Message
        public void ShowMessage(int _index)
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplateInput.ToList().ForEach(o => { o.SetActive(false); });
            cropImagePage.SetActive(false);
            GameManager.Instance.tutorial.tutorialPageGroup.SetActive(false);
            cameraZoomIMG.SetActive(false);
            cameraZoomMockIMG.ToList().ForEach(o => { o.SetActive(false); });
            animationTemplatePage.ToList().ForEach(o => { o.SetActive(false); });
            animationTemplatePage.ToList().ForEach(o => { o.SetActive(false); });
            bgIMGMessage.gameObject.SetActive(true);
            mockUpDragCard.GetComponent<DragPrefab>().Hide();
            mockUpDragCardUn.GetComponent<DragPrefabUn>().Hide();

            messagePages.GetComponent<CanvasGroup>().alpha = 1;
            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
            messageIndex = _index;

            messageButton.GetComponent<Button>().interactable = true;

            messageText[0].text = string.Empty;
            messageText[1].text = string.Empty;
            
            //Show Text Message ว่าอะไร
            //Message_0 = สองบรรทัด => แตะต่อไป
            //Message_1 = หนึ่งบรรทัด => แตะต่อไป
            //Message_2 = สองบรรทัด => ไม่มีแตะถัดไป
            //Message_3 = สามบรรทัด => มีแตะถัดไป
            //Message_4 = สามบรรทัด => ไม่มีแตะถัดไป
            //Message_5 = จะเล่นท่ายากเสือกไม่เอา
            //Message_6 = สี่บรรทัด => มีแตะถัดไป
            //Message_7 = สองบรรทัด => ว่างเปล่า
            //Message_8 = สามบรรทัด => คำถาม
            //Message_9 = ขึ้นพร้อมกันทั้งหมด => มีแตะถัดไป

            switch(_index)
            {
                case 0:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[0];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[1];
                    canvasAnim.Play("Message_0",0,0);
                    break;
                case 1:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[2];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[3];
                    canvasAnim.Play("Message_0",0,0);
                    break;
                case 2:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[4];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[5];
                    canvasAnim.Play("Message_0",0,0);
                    break;
                case 3:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[6];
                    canvasAnim.Play("Message_0",0,0);
                    break;
                case 7:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[7];
                    canvasAnim.Play("Message_1",0,0);
                    break; 
                case 8:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[8];
                    canvasAnim.Play("Message_1",0,0);
                    break;
                case 9:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[9];
                    canvasAnim.Play("Message_1",0,0);
                    break;
                case 10:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[10];
                    canvasAnim.Play("Message_1",0,0);
                    break;
                 case 11:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[11];
                    canvasAnim.Play("Message_1",0,0);
                    break;
                case 12:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[12];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[13];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[14];
                    messageText[3].text = gameManager.cardDatabaseSO.messages[0];
                    canvasAnim.Play("Message_9",0,0);
                    break;
                case 13:
                //คุณต้องการลงลึกมากขึ้นกับคุณค่าของเธอไหม? ไปต่อ พอแค่นี้
                    messageText[0].text = gameManager.cardDatabaseSO.messages[15];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[16];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[17];
                    canvasAnim.Play("Message_4",0,0);
                    break;
                case 14:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[14];
                    canvasAnim.Play("Message_2",0,0);
                    break;
                case 15:
                //ปุ่มสิ้นสุด ขอบคุณที่ให้โอกาสตัวเอง...
                    stageTemplateInput.ToList().ForEach(o => { o.SetActive(false); });
                    messageText[0].text = gameManager.cardDatabaseSO.messages[14];
                    canvasAnim.Play("Message_2",0,0);
                break;
                case 17:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[18];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[19];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[20];
                    canvasAnim.Play("Message_3",0,0);
                    break;
                case 18:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[21];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[22];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[23];
                    messageText[3].text = gameManager.cardDatabaseSO.messages[24];
                    canvasAnim.Play("Message_6",0,0);
                    messageIndex = 21;
                    break; 
                case 19:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[25];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[26];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[27];
                    canvasAnim.Play("Message_3",0,0);
                    messageIndex = 24;
                    break;
                case 20:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[33];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[34];
                    messageText[2].text = gameManager.cardDatabaseSO.messages[35];
                    canvasAnim.Play("Message_3",0,0);
                    messageIndex = 25;
                    break;      
                case 21:
                    messageText[0].text = gameManager.cardDatabaseSO.messages[37];
                    messageText[1].text = gameManager.cardDatabaseSO.messages[38];
                    canvasAnim.Play("Message_7",0,0);
                    break;             
            }
        }

        public int GetMessageIndex()
        {
            return messageIndex;
        }

        //หลังโชว์เสร็จกดแล้วจะไปไหน
        public void ButtonMessage()
        {
            //if(messageIndex == 14 || messageIndex == 13) return;
            messageButton.GetComponent<Button>().interactable = false;

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

                        bgIMGMessage.gameObject.SetActive(false);
                        bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1092);
                        bgIMGMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1092);
                        StartCoroutine(PlayAnimationThen("BGTransition_1", () =>{
                        messageButton.GetComponent<Button>().interactable = true;

                        messagePages.GetComponent<CanvasGroup>().alpha = 0;
                        messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;
                        //Open Tutorial
                        GameManager.Instance.tutorial.InitTutorial();
                        //gameManager.tutorial.tutorialPageGroup.SetActive(true);
                        //canvasGame.GetComponent<Animator>().Play("Tutorial",0,0);
                        }));
                        break;
                    case 4: Stage2();break;
                    case 5: Stage3();break;
                    case 6: Stage4();break;
                    case 7: ShowMessage(8);break;
                    case 8: OpenTemplate();break;
                    case 9: ShowMessage(10);break;
                    case 10: ShowMessage(11);break;
                    case 11: ShowMessage(12);break;
                    case 12: ShowMessage(13);break;
                    case 17: 
                        boxAnimateIndex = 1;
                        messageAnim = ShuffleList(gameManager.cardDatabaseSO.messagesAnim.ToList());
                        //messageAnimTemplateList_1 = ShuffleList(gameManager.cardDatabaseSO.messagesAnimTemplate_1.ToList());
                        //messageAnimTemplateList_2 = ShuffleList(gameManager.cardDatabaseSO.messagesAnimTemplate_2.ToList());
                        StageTemplateFade("Template-box_1");
                    break;
                    case 18:
                        canvasAnim.Play("Template-Massage_2",0,0);
                        messageIndex = 19;
                        StartCoroutine(UiController.Instance.WaitForSecond(10,()=>{
                            UiController.Instance.CanvasGroupFade(animationTemplatePage[1],true,1f);
                        }));
                    break;
                    case 19: //คลิกแล้ว หน้าเพสขึ้นมา
                        UiController.Instance.CanvasGroupFade(animationTemplatePage[2],true,1f);
                        animationTemplatePage[9].SetActive(true);
                        animationTemplatePage[10].SetActive(true);
                        animationTemplatePage[11].SetActive(true);
                        animationTemplatePage[9].GetComponent<TMPro.TextMeshProUGUI>().text = string.Format(GameManager.Instance.cardDatabaseSO.confirmList[boxAnimateIndex-1],"\n");
                        animationTemplatePage[10].GetComponent<TMPro.TextMeshProUGUI>().text = "เขียนเพิ่ม";
                        animationTemplatePage[11].GetComponent<TMPro.TextMeshProUGUI>().text = "ไปต่อ";
                        confirmPageIndex = 1;
                    break;
                    case 20: //คลิกแล้ว หน้าเพสขึ้นมา
                        UiController.Instance.CanvasGroupFade(animationTemplatePage[2],true,1f);
                        animationTemplatePage[9].SetActive(true);
                        animationTemplatePage[10].SetActive(true);
                        animationTemplatePage[11].SetActive(true);
                        animationTemplatePage[9].GetComponent<TMPro.TextMeshProUGUI>().text = "พร้อมจะไปต่อไหม?";
                        animationTemplatePage[10].GetComponent<TMPro.TextMeshProUGUI>().text = "ไม่พร้อม";
                        animationTemplatePage[11].GetComponent<TMPro.TextMeshProUGUI>().text = "พร้อม";                        
                        confirmPageIndex = 2;
                        break;
                    case 21: 
                        boxAnimateIndex = 4;
                        StageTemplateFade("Template-box_4");
                    break;
                    case 24: //เปิดหน้าสุดท้าย
                        OpenTemplateInput();
                    break;
                    case 25:
                        ShowMessage(21);
                        break;
                }

                //Debug.Log("🟢 กดปุ่มแล้ว" + messageIndex);

            List<MessageAnimTemplate> ShuffleList(List<MessageAnimTemplate> inputList)
            {
                List<MessageAnimTemplate> tempList = new List<MessageAnimTemplate>(inputList);
                for (int i = 0; i < tempList.Count; i++)
                {
                    int randomIndex = Random.Range(i, tempList.Count);
                    MessageAnimTemplate temp = tempList[i];
                    tempList[i] = tempList[randomIndex];
                    tempList[randomIndex] = temp;
                }
                return tempList;
            } 
        }

        private void StageTemplateFade(string _nameBox)
        {
                //bgIMGMessage.gameObject.SetActive(true);
                //bgIMGMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2162);
                int currentBoxAnimateIndex = boxAnimateIndex-1;
                GameObject currrentBox = templateBoxList[currentBoxAnimateIndex];
                mackupAminTemplateTX.text = currrentBox.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
                animationTemplatePage[7].SetActive(true);
                animationTemplatePage[7].GetComponent<TMPro.TMP_InputField>().text = string.Empty;
                animationTemplatePage[8].SetActive(true);
                animationTemplatePage[8].GetComponent<TMPro.TMP_InputField>().text = string.Empty;
                nameTextField_Stage4.placeholder.GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
                
                stageTemplate[1].SetActive(false);
                stageTemplate[2].SetActive(false);
                stageTemplate[3].SetActive(false);
                stageTemplate[4].SetActive(false);                
                stageTemplate[7].SetActive(true);                
                stageTemplate[8].SetActive(true);

                animationTemplatePage[5].GetComponent<TMPro.TextMeshProUGUI>().text = messageAnim[currentBoxAnimateIndex].text_1;
                animationTemplatePage[6].GetComponent<TMPro.TextMeshProUGUI>().text = messageAnim[currentBoxAnimateIndex].text_2;

                stageTemplate[0].GetComponent<CanvasGroup>().blocksRaycasts = false;
                stageTemplate[0].GetComponent<CanvasGroup>().alpha = 1;
                UiController.Instance.CanvasGroupFade(stageTemplate[0],true,2f,()=>
                {
                    StartCoroutine(UiController.Instance.WaitForSecond(1,()=>
                    {
                        animationTemplatePage[0].SetActive(true);
                       currrentBox.SetActive(false);

                        UiController.Instance.CanvasGroupFade(stageTemplate[0],false,3f);
                        StartCoroutine(PlayAnimationThen(_nameBox, () => {
                        canvasAnim.Play("Template-Massage",0,0);
                        messageIndex = 18;
                        }));
                    }));
                });
        }

        public void ConfirmPageButton()
        {
            switch(confirmPageIndex)
            {
                case 1:
                    animationTemplatePage[2].SetActive(false);
                    animationTemplatePage[1].SetActive(false);
                    canvasAnim.Play("Template-Massage_3",0,0);
                    StartCoroutine(UiController.Instance.WaitForSecond(10,()=>{
                        UiController.Instance.CanvasGroupFade(animationTemplatePage[3],true,1f);
                    }));
                    messageIndex = 20;
                break;
                case 2:
                    templateBoxSummaryList.Add(animationTemplatePage[8].GetComponent<TMPro.TMP_InputField>().text);
                    StartCoroutine(PlayAnimationThen("Template-Massage_4", () => {
                        animationTemplatePage.ToList().ForEach(o => { o.SetActive(false); });
                        boxAnimateIndex++;
                        if(boxAnimateIndex == 2) //1-2 เล่นปกติ
                        {
                           StageTemplateFade("Template-box_2");
                        }
                        else if(boxAnimateIndex == 3)
                        {
                           StageTemplateFade("Template-box_3");
                        }
                        else if(boxAnimateIndex == 4) //2-3 เล่นปกติ
                        {
                            ShowMessage(18);
                           //StageTemplateFade("Template-box_4");
                        }
                        else if(boxAnimateIndex == 5) 
                        {
                            StageTemplateFade("Template-box_5");
                        }
                        else if(boxAnimateIndex == 6)
                        {
                             ShowMessage(19);
                        } 
                    }));
                    animationTemplatePage[2].SetActive(false);
                    animationTemplatePage[3].SetActive(false);
                break;
            }
        }
        #endregion

        #region Gameplay
        private void Start()
        {
            gameManager = GameManager.Instance;
            #if UNITY_WEBGL && !UNITY_EDITOR
            WebGLInput.mobileKeyboardSupport = true; //ต้องมีไม่งั้นไอแพดเปิดแป้นพิมพ์ไม่ได้
            #endif
            cameraZoomGroup.SetActive(true);
            messageGroup.SetActive(true);
            ShowMessage(1);
        }

        private void Update()
        {
            if(currentStage == Stage.Stage4)
            {
                if(boxStageCount() == boxList.Count)
                {
                    myValueButton.interactable = true;
                }
                else
                {
                    myValueButton.interactable = false;
                }
            }

            if(currentStage == Stage.Template)
            {
               if (!hasStoppedBlink_6 && gameManager.cropImage.isUploadIMG)
                {
                    stageTemplate[6].GetComponent<UICanvasBlinker>().StopBlink();
                    stageTemplate[6].SetActive(false);
                    hasStoppedBlink_6 = true;
                }

                if (!hasStoppedBlink_5 && stageTemplate[8].GetComponent<TMPro.TMP_InputField>().text.Length > 0)
                {
                    stageTemplate[5].GetComponent<UICanvasBlinker>().StopBlink();
                    stageTemplate[5].SetActive(false);
                    hasStoppedBlink_5 = true;
                }

            }
        }

        public void Stage1()
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); o.GetComponent<CanvasGroup>().alpha = 1;});
            gameManager.tutorial.tutorialPageGroup.SetActive(false);
            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;

            fillBarGroup.SetActive(true);
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
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); o.GetComponent<CanvasGroup>().alpha = 1;});

            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;
            fillBarGroup.SetActive(true);

            currentStage = Stage.Stage2;
            headerText.text = gameManager.cardDatabaseSO.headers[1];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 400);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(45, 0);
            boxParent.GetComponent<GridLayoutGroup>().padding = new RectOffset(0, 0, 0, -40);// Left, Right, Top, Bottom

            timeObj.SetActive(true);
            NextStage(cardDataList_Stage1);
            InitStage(gameManager.cardDatabaseSO.boxsNameList_2,boxPrefab);
        }

        private void Stage3()
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); o.GetComponent<CanvasGroup>().alpha = 1;});

            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;
            fillBarGroup.SetActive(true);

            currentStage = Stage.Stage3;
            headerText.text = gameManager.cardDatabaseSO.headers[2];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(400, 400);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(45, 0);
            boxParent.GetComponent<GridLayoutGroup>().padding = new RectOffset(0, 0, 0, -40);// Left, Right, Top, Bottom

            timeObj.SetActive(true);
            NextStage(cardDataList_Stage2);
            InitStage(gameManager.cardDatabaseSO.boxsNameList_3 ,boxPrefab);
        }

         private void Stage4()
        {
            stageCardPages.ToList().ForEach(o => { o.SetActive(true); o.GetComponent<CanvasGroup>().alpha = 1;});
            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = false;
            fillBarGroup.SetActive(false);
            stageCardPriority.ToList().ForEach(o => { o.SetActive(true); });

            currentStage = Stage.Stage4;
            headerText.text = gameManager.cardDatabaseSO.headers[3];
            boxParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(350, 350);
            boxParent.GetComponent<GridLayoutGroup>().spacing = new Vector2(0, 0);

            gameManager.countdownTimer.StopCountdown();
            timeObj.SetActive(false);
            NextStage(cardDataList_Stage3);
            CreateBOX(gameManager.cardDatabaseSO.boxsNameList_4, boxPriorityPrefab);
            stageCardPriority[2].GetComponent<TMPro.TextMeshProUGUI>().text = "จำนวนการ์ดคุณค่าที่เลือก " + cardDataList.Count;

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

            gameManager.gridScrollController.RefreshGrid(cardDataList);
        }

        public void UninstallCardStage4(DropBox _dropbox,CardDataSO _cardDataSO)
        {
            cardDataList.Add(_cardDataSO);
            
            GameObject card = UiController.Instance.InstantiateUIView(cardPrefab, priorityParent);
            card.transform.SetAsLastSibling();

            card.name = _cardDataSO.name;
            card.GetComponent<Image>().sprite = _cardDataSO.picture;
            card.GetComponent<DragDropCard>().cardDataSO = _cardDataSO;

                // 🔥 เพิ่ม Effect Scale ด้วย DOTween
            card.transform.localScale = Vector3.zero; // เริ่มจาก 0
            card.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up
            _dropbox.transform.GetChild(0).GetComponent<Image>().sprite = gameManager.cardDatabaseSO.nullSprite;
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

                if(currentStage == Stage.Stage4) 
                {
                    box.GetComponent<DropBoxPriority>().dropName = o.boxName;
                    box.transform.GetChild(0).GetComponent<Image>().sprite = gameManager.cardDatabaseSO.nullSprite;
                }
                else 
                {
                    box.GetComponent<DropBox>().dropName = o.boxName;
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
            mockUpDragCard.GetComponent<DragPrefab>().Hide();
            gameManager.countdownTimer.StartCountdown();
        }

        public GameObject CreateCard(GameObject _prefab, GameObject _parent,CardDataSO _cardDataSO)
        {
            GameObject card = UiController.Instance.InstantiateUIView(_prefab ,_parent);
            card.name = _cardDataSO.name;
            card.GetComponent<Image>().sprite = _cardDataSO.picture;
            card.GetComponent<DragDropCard>().cardDataSO = _cardDataSO;

                // 🔥 เพิ่ม Effect Scale ด้วย DOTween
            card.transform.localScale = Vector3.zero; // เริ่มจาก 0
            card.transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack); // ค่อยๆ ขยายแบบ Pop-up
            return card;
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
/*
          private List<string> ShuffleList(List<string> inputList)
        {
            List<string> tempList = new List<string>(inputList);
            for (int i = 0; i < tempList.Count; i++)
            {
                int randomIndex = Random.Range(i, tempList.Count);
                string temp = tempList[i];
                tempList[i] = tempList[randomIndex];
                tempList[randomIndex] = temp;
            }
            return tempList;
        }*/

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
                cameraZoomMockIMG.ToList().ForEach(o => { o.SetActive(false); });
                switch(currentStage)
                {
                    //หลังจบด่าน 1 27
                    case Stage.Stage1:
                    ConditionStage(27,()=>{ 
                        gameManager.countdownTimer.StopCountdown();
                        StartCoroutine(UiController.Instance.WaitForSecond(3,()=>{
                            cameraZoomMockIMG[0].SetActive(true);
                            FadeBoxList(CameraZoom1);}));
                        importantBOX.cardDataSOList.ForEach(o => { cardDataList_Stage1.Add(o); });
                    });
                    void CameraZoom1()
                    {
                        cameraZoomIMG.SetActive(true);
                        cameraZoomMockIMG.ToList().ForEach(o => { o.SetActive(false); });

                        boxList.ForEach(o => { o.GetComponent<CanvasGroup>().alpha = 0; });
                        StartCoroutine(PlayAnimationThen("CameraZoom_1", () =>
                        {
                            cameraZoomIMG.SetActive(false);

                            messagePages.GetComponent<CanvasGroup>().alpha = 1;
                            messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;

                            bgIMGMessage.gameObject.SetActive(false);
                            bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 354);
                            bgIMGMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 354);
                            StartCoroutine(PlayAnimationThen("BGTransition_2", () =>
                            {
                                messageIndex = 4;
                                ButtonMessage();//=> ด่าน 2 
                            })); //PlayAnimationThen
                    })); //PlayAnimationThen
                    }
                    break;

                    //หลังจบด่าน 2 15
                    case Stage.Stage2:
                    ConditionStage(15,()=>{ 
                        gameManager.countdownTimer.StopCountdown();
                        StartCoroutine(UiController.Instance.WaitForSecond(3,()=>{
                            cameraZoomMockIMG[1].SetActive(true);
                            FadeBoxList(CameraZoom2);}));
                        importantBOX.cardDataSOList.ForEach(o => { cardDataList_Stage2.Add(o); });
                    });
                    void CameraZoom2()
                        {
                            cameraZoomIMG.SetActive(true);
                            cameraZoomMockIMG.ToList().ForEach(o => { o.SetActive(false); });
                            
                            boxList.ForEach(o => { o.GetComponent<CanvasGroup>().alpha = 0; });
                            StartCoroutine(PlayAnimationThen("CameraZoom_2", () =>
                            {
                                cameraZoomIMG.SetActive(false);

                                messagePages.GetComponent<CanvasGroup>().alpha = 1;
                                messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;

                                bgIMGMessage.gameObject.SetActive(false);
                                bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                                bgIMGMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                                StartCoroutine(PlayAnimationThen("BGTransition_3", () =>
                                {
                                    messageIndex = 5;
                                    ButtonMessage();//=> ด่าน 3 
                                }));

                            }));
                        }
                    break;

                    //หลังจบด่าน 3 10
                    case Stage.Stage3:
                    ConditionStage(10,()=>{ 
                        gameManager.countdownTimer.StopCountdown();
                        StartCoroutine(UiController.Instance.WaitForSecond(3,()=>{  
                            cameraZoomMockIMG[2].SetActive(true); FadeBoxList(CameraZoom3);}));
                        importantBOX.cardDataSOList.ForEach(o => { cardDataList_Stage3.Add(o); });
                    });
                        /*
                        //เช็คว่า ขาดไม่ได้ จะต้องไม่เกิน 15 ใบ
                        if(importantBOX.cardDataSOList.Count < 5 || importantBOX.cardDataSOList.Count > 15)
                        {
                            Debug.Log("🟢 ขาดไม่ได้ เกิน 15 ใบ เล่นใหม่");
                            Stage3();
                        }*/

                    void CameraZoom3()
                            {
                                Debug.Log("🟢 ขาดไม่ได้ ไม่เกิน 15 ใบ ไปด่านต่อไป");
                                cameraZoomIMG.SetActive(true);
                                cameraZoomMockIMG.ToList().ForEach(o => { o.SetActive(false); });

                                boxList.ForEach(o => { o.GetComponent<CanvasGroup>().alpha = 0; });
                                StartCoroutine(PlayAnimationThen("CameraZoom_3", () =>
                                {
                                    cameraZoomIMG.SetActive(false);

                                    messagePages.GetComponent<CanvasGroup>().alpha = 1;
                                    messagePages.GetComponent<CanvasGroup>().blocksRaycasts = true;

                                    bgIMGMessage.gameObject.SetActive(false);
                                    bgIMG.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2162);
                                    bgIMGMessage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -2162);
                                    StartCoroutine(PlayAnimationThen("BGTransition_4", () =>
                                    {
                                        messageIndex = 6;
                                        ButtonMessage();//=> ด่าน 4 
                                    }));
                                }));
                            }                        
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

            void ConditionStage(int _amount, System.Action _onComplete)
            {
                int requiredCount = _amount;
                int currentCount = importantBOX.cardDataSOList.Count;

                Debug.Log($"[เริ่มด่าน 1] กล่องหลักมี {currentCount} ใบ");

                // ลูปกล่องที่เหลือ เพื่อเติมการ์ดให้ถึง x ใบ
                for (int i = 1; i < boxList.Count && currentCount < requiredCount; i++)
                {
                    var dropBox = boxList[i].GetComponent<DropBox>();
                    int cardsToAdd = dropBox.cardDataSOList.Count;

                    importantBOX.cardDataSOList.AddRange(dropBox.cardDataSOList);
                    currentCount = importantBOX.cardDataSOList.Count;

                    Debug.Log($"➡️ เพิ่มจากกล่อง {i + 1} จำนวน {cardsToAdd} ใบ | รวม = {currentCount} ใบ");
                }

                // ตรวจสอบว่าผ่านหรือไม่
                if (currentCount >= requiredCount)
                {
                    Debug.Log($"✅ ผ่านด่าน : รวมทั้งหมด {currentCount} ใบ (ครบ {_amount} แล้ว)");
                    //รอสามวิก่อน
                    _onComplete?.Invoke();                                                                                                                                                     
                }
                else
                {
                    Debug.LogWarning($"❌ ยังไม่ผ่านด่าน : รวมได้ {currentCount} ใบ (ยังไม่ถึง {_amount})");
                }
            }

            void FadeBoxList(System.Action onComplete)
            {
                stageCardPages[0].GetComponent<CanvasGroup>().DOFade(0,2f);
                
                var seq = DOTween.Sequence();

                foreach (var o in boxList)
                {
                    //if (o == importantBOX.gameObject) continue;

                    CanvasGroup cg = o.GetComponent<CanvasGroup>();
                    if (cg != null)
                    {
                        seq.Join(cg.DOFade(0f, 2f).SetEase(Ease.InOutSine));
                    }
                }

                seq.OnComplete(() => { onComplete(); });
            
        }
        }
        
        private void NextStage(List<CardDataSO> _cardDataSOs)
        {
            cardDataList.Clear();
            _cardDataSOs.ToList().ForEach(o =>
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
            canvasAnim.Play(animationName,0,0);

            // รอจนกว่าอนิเมชั่นจะหยุด
            yield return null;
            AnimatorStateInfo stateInfo = canvasAnim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);

            onComplete?.Invoke();
        }

        //นับจำนวนไพ่ที่อยู่ในกล่องแล้ว ใน stage4
        public int boxStageCount()
        {
            int count = 0;
            boxList.ToList().ForEach(o =>
            {
               if( o.GetComponent<DropBoxPriority>().cardName_Stage4 != null)
                {
                    count++;
                }
            });
            return count;
        }

        public void OpenTemplate()
        {
            currentStage = Stage.Template;
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplateInput.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(true); });
            stageTemplate[3].SetActive(false);
            stageTemplate[4].SetActive(true);
            stageTemplate[5].GetComponent<UICanvasBlinker>().StartBlink();
            stageTemplate[6].GetComponent<UICanvasBlinker>().StartBlink();
            stageTemplate[3].GetComponent<CanvasGroup>().alpha = 0;
            stageTemplate[0].GetComponent<CanvasGroup>().blocksRaycasts = true;

            int index = 0;
            boxList.ToList().ForEach(o =>
            {
                if(o.GetComponent<DropBoxPriority>().cardName_Stage4 != null)
                {
                    priorityListTexts[index].text = string.Format(o.GetComponent<DropBoxPriority>().cardName_Stage4.cardTH,"\n");
                }
                else
                {
                    priorityListTexts[index].text = string.Empty;
                }
                index++;
            });


            CanvasGroup canvasGroup = stageTemplate[0].GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, 0.5f).OnComplete(()=>{});

        }

        public void OpenTemplateInput()
        {
            currentStage = Stage.TemplateInput;
            stageCardPriority.ToList().ForEach(o => { o.SetActive(false); });
            stageCardPages.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplate.ToList().ForEach(o => { o.SetActive(false); });
            stageTemplateInput.ToList().ForEach(o => { o.SetActive(true); });
            
            nameTextField_Stage5.text = nameTextField_Stage4.text;

            animationTemplatePage.ToList().ForEach(o => { o.SetActive(false); });
                UiController.Instance.CanvasGroupFade(stageTemplateInput[0],true,1f);
                stageTemplateInput[1].SetActive(false);

                int index = 0;
                priorityLastSummaryListTexts.ToList().ForEach(o => { 
                o.text = templateBoxSummaryList[index];
                index++;
                });

                int index2 = 0;
                priorityLastListTexts.ToList().ForEach(o => { 
                    o.text = priorityListTexts[index2].text;
                    index2++;
                });

            //Open Tutorial
            //gameManager.tutorial.InitTutorial2();
        }

        public void EndStage4()
        {
            ShowMessage(7);
        }

        public void EndTemplete()
        {
            ShowMessage(9);
        }
      #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Chunking
{
    public enum ChankingType
    {
        Easy,
        Normal
    }

    public enum WayType
    {
        Up,
        Down,
        Sideway
    }

    [System.Serializable]
    public class ChunkingData
    {
        public WayType type;
        public Sprite normal_SP;
        public Sprite selected_SP;
        public Sprite show_SP;
        public Sprite showGameplay_SP;
        public string[] descriptions;
    }

    [System.Serializable]
    public class ChunkingShowData
    {
        public ChunkingSO chunkingSO;
        public WayType type;
        public ChunkingShowPrefab showSlot;

        public ChunkingShowData(ChunkingShowPrefab _showSlot)
        {
            showSlot = _showSlot;
        }
    }

    public class LevelManager : MonoBehaviour
    {
        public DatabaseSO databaseSO;
        [Header("Right")]
        public GameObject parent;
        public GameObject slotPrefab;
        [Header("Left")]
        public GameObject parentLeft;
        public GameObject slotPrefabLeft;
        public Color colorSelected_1;
        public Color colorSelected_2;

        public List<ChunkingShowData> showDataList = new List<ChunkingShowData>();
        
        [Header("Clicked")]
        public ChunkingShowPrefab currentShowPrefab;
        public ChunkingSelectPrefab currentSelectPrefab;
        public WayType currentwayType;
        List<Button> showSlots = new List<Button>();
        List<ChunkingShowData> showResultDataList = new List<ChunkingShowData>();
        [Header("Gameplay")]
        public int currentDataIndex;

       public void InitSelectSlot()
       {
            UiController.Instance.DestorySlot(parent);
            int index = 0;
            databaseSO.chunkingSO.ToList().ForEach(o =>{
                GameObject slot = UiController.Instance.InstantiateUIView(slotPrefab,parent);
                slot.GetComponent<ChunkingSelectPrefab>().SetSlot(o);
                slot.GetComponent<ButtonGroup>().key = index.ToString();
                if(index == 0) currentSelectPrefab = slot.GetComponent<ChunkingSelectPrefab>();
                index++;
            });
       }

       public void InitShowSlot()
       {
            UiController.Instance.DestorySlot(parentLeft);
            int indexColor = 0;
            showDataList.Clear();
            for(int i = 0 ; i< 5 ; i++)
            {
                ChunkingShowPrefab slot = UiController.Instance.InstantiateUIView(slotPrefabLeft,parentLeft).GetComponent<ChunkingShowPrefab>();
                slot.index = i;
                slot.SetDefault();
                if(indexColor == 0)
                {
                    slot.SetBGColor(colorSelected_1);
                    indexColor = 1;
                }
                else 
                {
                    slot.SetBGColor(colorSelected_2);
                    indexColor = 0;
                }
                slot.name = i.ToString();
                slot.GetComponent<ButtonGroup>().key = i.ToString();
                showDataList.Add(new ChunkingShowData(slot));
                slot.GetComponent<Button>().interactable = false;
                showSlots.Add(slot.GetComponent<Button>());
            }
            SetIndexShowSlot(0);
            SetFristLobby();
            //ตัวอย่าง Slot แรก
            currentSelectPrefab.GetComponent<Button>().onClick.Invoke();
       }
       
       public void SetFristLobby()
       {
            currentShowPrefab = showDataList[0].showSlot;
            ButtonGroupManager.Instance.Select(currentShowPrefab.GetComponent<ButtonGroup>());   
       }

       public void ClickSelected(ChunkingSelectPrefab _chunkingSelectPrefab)
       {
            currentSelectPrefab = _chunkingSelectPrefab;
            if(currentShowPrefab != null) 
            {
                if(currentShowPrefab.chunkingSO == null)
                {
                    currentwayType = WayType.Up;
                    Button btnChar = ButtonGroupManager.Instance.GetButton("ChunkingWayType", "Up");
                    if (btnChar != null) ButtonGroupManager.Instance.Select(btnChar.GetComponent<ButtonGroup>());
                }
                currentShowPrefab.SetSlot(_chunkingSelectPrefab.chunkingSO,currentwayType);
            }
       }

       public void SetWayType(WayType _wayType)
       {
            currentwayType = _wayType;
            if(currentShowPrefab != null) 
            {
                currentShowPrefab.SetType(_wayType);
            }
            if(currentShowPrefab.chunkingSO == null)
            {
                if(currentSelectPrefab != null)
                {
                    currentSelectPrefab.ClickButton();
                }
            }
       }
       
       //ใส่ตามลำดับ
       public void SetIndexShowSlot(int _currentIndex)
       {
            if (_currentIndex < showSlots.Count)
            {
                showSlots[_currentIndex].interactable = true;
            }

            if (_currentIndex + 1 < showSlots.Count)
            {
                showSlots[_currentIndex + 1].interactable = true;
            }
       }

        #region  GamePlay
        public void StartGameClick()
        {
            showResultDataList.Clear();
            showDataList.ForEach(o => {
                o.chunkingSO = o.showSlot.chunkingSO;
                o.type = o.showSlot.wayType;
                if(o.chunkingSO != null) showResultDataList.Add(o);
            });

            if(showResultDataList.Count > 0)
            {
                GameManager.Instance.uIGameManager.ShowGamePlay(showResultDataList[0]);
            }
            currentDataIndex = 0;
            GameManager.Instance.uIGameManager.nextGO.SetActive(true);
            GameManager.Instance.uIGameManager.backGO.SetActive(false);
            if(showResultDataList.Count <= 1) GameManager.Instance.uIGameManager.nextGO.SetActive(false);
       }
       
       public void NextClick()
       {
            if (currentDataIndex < showResultDataList.Count - 1) currentDataIndex += 1;
            else  currentDataIndex = showResultDataList.Count - 1;
            GameManager.Instance.uIGameManager.ShowGamePlay(showResultDataList[currentDataIndex]);
            
            //Index สุดท้าย
            if (currentDataIndex == showResultDataList.Count - 1)
            {
                GameManager.Instance.uIGameManager.nextGO.SetActive(false);
            }
            else GameManager.Instance.uIGameManager.backGO.SetActive(true);
       }

       public void BackClick()
       {
            currentDataIndex -=1;
            if(currentDataIndex <= 0) 
            {
                currentDataIndex = 0;
                GameManager.Instance.uIGameManager.nextGO.SetActive(true);
                GameManager.Instance.uIGameManager.backGO.SetActive(false);
            }
            GameManager.Instance.uIGameManager.ShowGamePlay(showResultDataList[currentDataIndex]);
       }
    #endregion
    }
}

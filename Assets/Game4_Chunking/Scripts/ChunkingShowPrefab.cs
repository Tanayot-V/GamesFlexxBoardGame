using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chunking
{
    public class ChunkingShowPrefab : MonoBehaviour
    {
        public int index;
        public ChunkingSO chunkingSO;
        public WayType wayType;
        public Image bgIMG;
        public Image cardIMG;
        public Image wayIMG;
        
        public void SetSlot(ChunkingSO _chunkingSO,WayType _wayType)
        {
            chunkingSO = _chunkingSO;
            wayType = _wayType;
            cardIMG.gameObject.SetActive(true);
            wayIMG.gameObject.SetActive(true);
            cardIMG.sprite = _chunkingSO.picture;
            wayIMG.sprite = GameManager.Instance.levelManager.databaseSO.GetChunkingData(_wayType).normal_SP;
            GameManager.Instance.levelManager.SetIndexShowSlot(index);
        }

        public void SetType(WayType _wayType)
        {
            wayIMG.gameObject.SetActive(true);
            wayIMG.sprite = GameManager.Instance.levelManager.databaseSO.GetChunkingData(_wayType).normal_SP;
            wayType = _wayType;

                Button btnChar = ButtonGroupManager.Instance.GetButton("ChunkingWayType", "Up"); 
                switch(_wayType)
                {
                    case WayType.Up:
                    btnChar = ButtonGroupManager.Instance.GetButton("ChunkingWayType", "Up");
                    break;
                    case WayType.Down:
                    btnChar = ButtonGroupManager.Instance.GetButton("ChunkingWayType", "Down");
                    break;
                    case WayType.Sideway:
                    btnChar = ButtonGroupManager.Instance.GetButton("ChunkingWayType", "Sideway");
                    break;
                }
                if (btnChar != null) ButtonGroupManager.Instance.Select(btnChar.GetComponent<ButtonGroup>());

        }

        public void SetBGColor(Color _color)
        {
            bgIMG.color = _color;
        }

        public void SetDefault()
        {
           cardIMG.gameObject.SetActive(false);
           wayIMG.gameObject.SetActive(false);
        }

        public void SetCurrentShowSlot()
        {
            GameManager.Instance.levelManager.currentShowPrefab = this;
            GameManager.Instance.levelManager.SetWayType(wayType);
        }
        
    }
}

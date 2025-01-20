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
            wayIMG.sprite = GameManager.Instance.levelManager.databaseSO.GetChunkingData(_wayType).show_SP;
        }


        public void SetType(WayType _wayType)
        {
            wayIMG.gameObject.SetActive(true);
            wayIMG.sprite = GameManager.Instance.levelManager.databaseSO.GetChunkingData(_wayType).show_SP;
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
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chunking
{
    public class ChunkingSelectPrefab : MonoBehaviour
    {
        public ChunkingSO chunkingSO;
        public Image showIMG;
        public GameObject selectedIMG;
        public GameObject starGO;

        public void SetSlot(ChunkingSO _chunkingSO)
        {
            chunkingSO = _chunkingSO;
            showIMG.sprite = _chunkingSO.picture;
            if(_chunkingSO.type == ChankingType.Easy) starGO.SetActive(true);
            else starGO.SetActive(false);
            selectedIMG.gameObject.SetActive(false);
        }

        public void ClickButton()
        {
            GameManager.Instance.levelManager.ClickSelected(this);
        }
    }
}

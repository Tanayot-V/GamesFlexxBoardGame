using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chunking
{
    [CreateAssetMenu(fileName = "ChunkingSO", menuName = "Chunking/ChunkingSO", order = 1)]
    public class ChunkingSO : ScriptableObject
    {
        public Sprite picture;
        public ChankingType type;
        
        public string[] chunkUP_Quests;
        public string[] chunkDown_Quests;
        public string[] chunkSideway_Quests;
    }
}

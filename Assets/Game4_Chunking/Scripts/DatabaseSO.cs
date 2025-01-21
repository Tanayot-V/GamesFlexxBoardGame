using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Chunking
{
    [CreateAssetMenu(fileName = "DatabaseSO", menuName = "Chunking/DatabaseSO", order = 1)]
    public class DatabaseSO : ScriptableObject
    {
        public ChunkingData[] chunkingDatas;
       public ChunkingSO[] chunkingSO;

        private Dictionary<WayType,ChunkingData> chankingDatasDIC = new Dictionary<WayType, ChunkingData>();
        public ChunkingData GetChunkingData(WayType _type)
        {
            if (chankingDatasDIC.ContainsKey(_type))
            {
                return chankingDatasDIC[_type];
            }
            else
            {
                return chankingDatasDIC[_type] = chunkingDatas.ToList().Find(o => o.type == _type);
            }
        }
    }
}

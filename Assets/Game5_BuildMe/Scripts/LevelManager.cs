using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BuildMe
{
    public class LevelManager : MonoBehaviour
    {
        public List<string> items = new List<string>();
        public List<string> functions = new List<string>();
        public List<string> targets = new List<string>();

        // Start is called before the first frame update
        void Start()
        {
            // เรียกใช้การสุ่มโดยไม่ให้ซ้ำ
            var selectedItems = GetRandomElements(GameManager.Instance.innovationDatabase.items, 3);
            var selectedFunctions = GetRandomElements(GameManager.Instance.innovationDatabase.functions, 2);
            var selectedTargets = GetRandomElements(GameManager.Instance.innovationDatabase.targets, 1);

            // แสดงผล
            Debug.Log("Selected Items: " + string.Join(", ", selectedItems));
            Debug.Log("Selected Functions: " + string.Join(", ", selectedFunctions));
            Debug.Log("Selected Targets: " + string.Join(", ", selectedTargets));
        }

        private List<T> GetRandomElements<T>(List<T> sourceList, int count)
        {
            if (sourceList.Count < count)
            {
                Debug.LogError("จำนวนรายการในลิสต์น้อยกว่าจำนวนที่ต้องการสุ่ม!");
                return new List<T>();
            }

            return sourceList.OrderBy(x => Random.value).Take(count).ToList();
        }
    }
}

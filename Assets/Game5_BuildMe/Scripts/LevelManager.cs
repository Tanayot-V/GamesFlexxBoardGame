using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace BuildMe
{
    public class LevelManager : MonoBehaviour
    {
        public List<string> selectedItems = new List<string>();
        public List<string> selectedFunctions = new List<string>();
        public List<string> selectedTargets = new List<string>();

        void Start(){ }

        public void SetRandom()
        {
            //เรียกใช้การสุ่มโดยไม่ให้ซ้ำ
            selectedItems = GetRandomElements(GameManager.Instance.innovationDatabase.items, 3);
            selectedFunctions = GetRandomElements(GameManager.Instance.innovationDatabase.functions, 2);
            selectedTargets = GetRandomElements(GameManager.Instance.innovationDatabase.targets, 1);

            // แสดงผล
            Debug.Log("Selected Items: " + string.Join(", ", selectedItems));
            Debug.Log("Selected Functions: " + string.Join(", ", selectedFunctions));
            Debug.Log("Selected Targets: " + string.Join(", ", selectedTargets));

            GameManager.Instance.uiGameManager.SetImgs(selectedItems.ToArray(), selectedFunctions.ToArray(), selectedTargets.ToArray());
            List<T> GetRandomElements<T>(List<T> sourceList, int count)
            {
                if (sourceList.Count < count)
                {
                    Debug.LogError("จำนวนรายการในลิสต์น้อยกว่าจำนวนที่ต้องการสุ่ม!");
                    return new List<T>();
                }

                return sourceList.OrderBy(x => Random.value).Take(count).ToList();
            }

            GameManager.Instance.uiGameManager.transitionPanel.SetActive(true);
            var setEventVideo = GameManager.Instance.uiGameManager.animator.GetComponent<SetEventVideo>();
            setEventVideo.Init();
            setEventVideo.displayShow.Clear();
            selectedItems.ToList().ForEach(x => setEventVideo.displayShow.Add(x));
            selectedFunctions.ToList().ForEach(x => setEventVideo.displayShow.Add(x));
            selectedTargets.ToList().ForEach(x => setEventVideo.displayShow.Add(x));
            setEventVideo.AtTextAllLists();
            
        }
    }
}

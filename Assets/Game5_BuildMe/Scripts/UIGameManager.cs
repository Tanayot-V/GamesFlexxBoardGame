using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace BuildMe
{
    public class UIGameManager : MonoBehaviour
    {
        [SerializeField] GameObject lobbyPanel;
        [SerializeField] GameObject gamePanel;
        [SerializeField] GameObject loadingPanel;
        [SerializeField] TextMeshProUGUI[] itemsIMG;
        [SerializeField] TextMeshProUGUI[] functionsIMG;
        [SerializeField] TextMeshProUGUI[] targetsIMG;

        [Header("Transition")]
        public GameObject transitionPanel;
        public Animator animator;
        private List<string> transitionNames = new List<string>
    {
        "Transition1", "Transition2", "Transition3", "Transition4",
        "Transition5", "Transition6", "Transition7", "Transition8"
    };
        public TextMeshProUGUI transitionText; 

        public void ShowLobbyPanel()
        {
            lobbyPanel.SetActive(true);
            gamePanel.SetActive(false);
            transitionPanel.SetActive(false);
        }
        public void ShowGamePanel()
        {
            lobbyPanel.SetActive(false);
            gamePanel.SetActive(true);
            GameManager.Instance.levelDataManager.SetRandom();
        }
        public void SetImgs(string[] items, string[] functions, string[] targets)
        {
            for (int i = 0; i < itemsIMG.Length; i++)
            {
                itemsIMG[i].text = items[i];
            }
            for (int i = 0; i < functionsIMG.Length; i++)
            {
                functionsIMG[i].text = functions[i];
            }
            for (int i = 0; i < targetsIMG.Length; i++)
            {
                targetsIMG[i].text = targets[i];
            }
        }
        public void ShowLoading(System.Action _action = null)
        {
            loadingPanel.SetActive(true);
            StartCoroutine(UiController.Instance.WaitForSecond(1,()=>{
                loadingPanel.SetActive(false);
                if(_action != null) _action(); 
            }));
        }

       public IEnumerator PlayRandomTransitions()
        {
            // สุ่มแอนิเมชัน 6 ตัวโดยไม่ซ้ำกัน
            List<string> randomTransitions = transitionNames.OrderBy(x => Random.value).Take(6).ToList();

            for (int i = 0; i < randomTransitions.Count; i++)
            {
                string selectedTransition = randomTransitions[i];

                // แสดงข้อความของแอนิเมชันที่เลือก
                transitionText.text = "Now Playing: " + selectedTransition;

                // เล่นแอนิเมชัน
                animator.Play(selectedTransition);
                Debug.Log("Playing: " + selectedTransition);

                // รอให้แอนิเมชันเล่นจนจบแล้วรอ 2 วินาที
                yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.125f);
            }

            Debug.Log("All animations played!");
        }
    }
}

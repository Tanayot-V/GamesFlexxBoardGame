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
        [SerializeField] Animator bgAnimator;
         private int transitionCount = 5;
        [SerializeField] TextMeshProUGUI[] itemsIMG;
        [SerializeField] TextMeshProUGUI[] functionsIMG;
        [SerializeField] TextMeshProUGUI[] targetsIMG;

        [Header("Transition")]
        public GameObject transitionPanel;
        public Animator animator;

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
    }
}

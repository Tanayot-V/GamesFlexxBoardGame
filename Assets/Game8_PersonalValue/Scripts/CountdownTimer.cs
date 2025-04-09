using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PersonalValue
{
    public class CountdownTimer : MonoBehaviour
    {
         [SerializeField] private Image fillImage;
        private float duration = 30f;
        [SerializeField] private TMPro.TextMeshProUGUI timeText;

        private Coroutine countdownCoroutine;

        // 👉 เริ่มนับถอยหลัง
        public void StartCountdown()
        {

            StopCountdown(); // หยุดก่อนถ้ามีการทำงานอยู่
            countdownCoroutine = StartCoroutine(Countdown());
            switch (GameManager.Instance.levelManager.currentStage)
            {
                case Stage.Stage1:
                    duration = 30f;
                    break;
                case Stage.Stage2:
                    duration = 40f; 
                    break;
                case Stage.Stage3:
                    duration = 40f;
                    break;
            }

            /*
            countdownTimer.StartCountdown(); // เริ่มใหม่
            countdownTimer.StopCountdown();  // หยุด*/
        }

        // 👉 หยุดนับถอยหลัง
        public void StopCountdown()
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
                Debug.Log("⛔️ หยุดการนับถอยหลัง");
            }
        }

        private IEnumerator Countdown()
        {
            float remainingTime = duration;
            Debug.Log("🟢 เริ่มนับถอยหลัง " + duration + " วินาที");

            while (remainingTime > 0f)
            {
                fillImage.fillAmount = Mathf.Clamp01(remainingTime / duration);
                timeText.text = Mathf.CeilToInt(remainingTime).ToString();

                remainingTime -= Time.deltaTime;
                yield return null;
            }

            fillImage.fillAmount = 0;
            timeText.text = "0";

            Debug.Log("🔔 Cooldown หมดแล้ว!");
            GameManager.Instance.levelManager.RerollCard(); // เรียกใช้ฟังก์ชัน RerollCard() ที่อยู่ใน LevelManager
            countdownCoroutine = null; // เคลียร์ค่า
        }
    }
}

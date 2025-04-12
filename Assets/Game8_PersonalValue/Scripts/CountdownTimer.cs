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
            switch (GameManager.Instance.levelManager.currentStage)
            {
                case Stage.Stage1:
                    duration = 10f;
                    break;
                case Stage.Stage2:
                    duration = 10f; 
                    break;
                case Stage.Stage3:
                    duration = 10f;
                    break;
            }
            countdownCoroutine = StartCoroutine(Countdown());
        }

        // 👉 หยุดนับถอยหลัง
        public void StopCountdown()
        {
            Debug.Log("StopCountdown");
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
            countdownCoroutine = null; // เคลียร์ค่า
            GameManager.Instance.levelManager.RerollCard(); // เรียกใช้ฟังก์ชัน RerollCard() ที่อยู่ใน LevelManager

        }
    }
}

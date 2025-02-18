using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuildMe
{
    [CreateAssetMenu(fileName = "DailyIntention", menuName = "Innovation/DatabaseSO", order = 0)]
    public class InnovationDatabaseSO : ScriptableObject 
    {
        public TextAsset csv;
        public List<string> items = new List<string>();
        public List<string> functions = new List<string>();
        public List<string> targets = new List<string>();

        public void LoadDataFromCSV()
        {
            items.Clear();
            functions.Clear(); 
            targets.Clear();

            if (csv == null)
            {
                Debug.LogError("CSV file is missing!");
                return;
            }

            string[] lines = csv.text.Split('\n');

            for (int i = 1; i < lines.Length; i++) // ข้ามแถวแรกที่เป็น header
            {
                string[] values = lines[i].Split(',');

                if (values.Length >= 6)  // ตรวจสอบให้แน่ใจว่ามีอย่างน้อย 6 คอลัมน์
                {
                    items.Add(values[1].Trim());  // ดึงข้อมูลจากคอลัมน์ที่ 2
                    functions.Add(values[3].Trim());    // ดึงข้อมูลจากคอลัมน์ที่ 4
                    targets.Add(values[5].Trim());    // ดึงข้อมูลจากคอลัมน์ที่ 6
                }
            }

            items.RemoveAll(item => string.IsNullOrWhiteSpace(item));
            functions.RemoveAll(item => string.IsNullOrWhiteSpace(item));
            targets.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            Debug.Log("CSV Loaded Successfully. Total Items: " + items.Count);
        }
    }
}

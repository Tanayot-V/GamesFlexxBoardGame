using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IconDatabase", menuName = "ScriptableObjects/IconDatabase", order = 1)]
public class IconDataPool : ScriptableObject
{
    public List<IconData> iconDatas = new List<IconData>();
    public List<IconData> GetRandomIconDatas(int amount = 1, List<string> excludeName = null)
    {
        List<IconData> result = new List<IconData>();
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, iconDatas.Count);
            if ((excludeName != null && excludeName.Contains(iconDatas[randomIndex].name)) || result.Contains(iconDatas[randomIndex]))
            {
                i--;
                continue;
            }
            result.Add(iconDatas[randomIndex]);
        }
        return result;
    }
}

[System.Serializable]
public class IconData
{
    public string name;
    public Sprite icon;
}

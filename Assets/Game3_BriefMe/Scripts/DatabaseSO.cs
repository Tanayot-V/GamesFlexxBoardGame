using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BriefMe
{
    [CreateAssetMenu(fileName = "DatabaseSO", menuName = "BriefMe/DatabaseSO", order = 1)]
    public class DatabaseSO : ScriptableObject
    {
       public Sprite[] picturesSP;
       public Sprite[] linesSP;

       public Sprite GetRandomPicture()
       {
        int randomIndex = Random.Range(0, picturesSP.Length);
        return picturesSP[randomIndex];
       }

        public Sprite GetRandomLine()
       {
        int randomIndex = Random.Range(0, linesSP.Length);
        return linesSP[randomIndex];
       }
    }
}

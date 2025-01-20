using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BriefMe
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] DatabaseSO databaseSO;
        public void PictureShow(Image _image)
        {
            _image.sprite = databaseSO.GetRandomPicture();
        }
         public void LineShow(Image _image)
        {
            _image.sprite = databaseSO.GetRandomLine();
        }
    }
}

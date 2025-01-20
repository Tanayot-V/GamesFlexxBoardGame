using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BriefMe
{
    public class GameManager : Singletons<GameManager>
    {
        public LevelManager levelManager;
        public UIGameManager uIGameManager;
        
        void Start()
        {
            uIGameManager.InitStartGameUI();
            uIGameManager.ShowLoading();
        }

        void Update()
        {
            
        }
    }
}

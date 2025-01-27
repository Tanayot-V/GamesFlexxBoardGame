using System.Collections;
using System.Collections.Generic;
using SpotTheMissing;
using UnityEngine;

namespace GodWarShip
{
    public enum Mode
    {
        Easy,
        Normal,
        Hard
    }
    public class GameManager : Singletons<GameManager>
    {
        public LevelDataManager levelDataManager;
        public UIGameManager uIGameManager;

        void Start()
        {
            /*
            levelDataManager.ClearCards();
            levelDataManager.InitGame();
            levelDataManager.RandomCard();
            */
        }

        void Update()
        {
           
        }
    }
}

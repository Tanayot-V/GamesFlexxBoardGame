using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chunking
{
    public class GameManager : Singletons<GameManager>
    {
        public LevelManager levelManager;
        public UIGameManager uIGameManager;

        void Start()
        {
            uIGameManager.InitStartGame();
            levelManager.InitSelectSlot();
            levelManager.InitShowSlot();
        }

        void Update()
        {
            
        }
    }
}

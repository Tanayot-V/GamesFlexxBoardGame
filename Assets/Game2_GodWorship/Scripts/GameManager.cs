using System.Collections;
using System.Collections.Generic;
using SpotTheMissing;
using UnityEngine;

namespace GodWarShip
{
    public class GameManager : Singletons<GameManager>
    {
        public LevelDataManager levelDataManager;
        public UIGameManager uIGameManager;

        void Start()
        {
             levelDataManager.ClearCards();
                levelDataManager.InitGame();
                levelDataManager.RandomCard();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                levelDataManager.ClearCards();
                levelDataManager.InitGame();
                levelDataManager.RandomCard();
                /*
                StartCoroutine(                
                    UiController.Instance.WaitForSecond(1 ,()=> {
                    levelDataManager.RandomCard();
                }));*/
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using GodWarShip;
using Unity.VisualScripting;
using UnityEngine;

namespace BuildMe
{
    public class GameManager : Singletons<GameManager>
    {
        public InnovationDatabaseSO innovationDatabase;
        public LevelManager levelDataManager;
        public UIGameManager uiGameManager;
      public void Start()
      {
        //innovationDatabase.LoadDataFromCSV();
        uiGameManager.ShowLobbyPanel();
      }
    }
}

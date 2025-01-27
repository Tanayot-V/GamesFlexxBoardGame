using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BuildMe
{
    public class GameManager : Singletons<GameManager>
    {
        public InnovationDatabaseSO innovationDatabase;
      public void Start()
      {
        //innovationDatabase.LoadDataFromCSV();
      }
    }
}

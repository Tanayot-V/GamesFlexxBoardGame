using System.Collections;
using System.Collections.Generic;
using GodWarShip;
using UnityEngine;

namespace PersonalValue
{
public class GameManager : Singletons<GameManager>
{
    public PersonalValueDatabaseSO cardDatabaseSO;
    public LevelManager levelManager;
    public CountdownTimer countdownTimer;
    public  CropImage cropImage;
    public WebGLFileLoader webGLFileLoader;
    
    public WebGLFileLoaderButton webGLFileLoaderButton;
    public Tutorial tutorial;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
}
